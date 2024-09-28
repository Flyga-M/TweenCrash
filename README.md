# TweenCrash

A minimal example module to reproduce the following `Tween` crashes in Blish HUD:
- ```csharp
   System.IndexOutOfRangeException: Index was outside the bounds of the array.
   at Glide.Tween.Update(Single elapsed) in D:\a\Blish-HUD\Blish-HUD\Blish HUD\Library\Glide\Tween.cs:line 79```
- ```csharp
   System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
   at System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
   at System.Collections.Generic.List`1.Enumerator.MoveNextRare()
   at System.Linq.Enumerable.Any[TSource](IEnumerable`1 source)
   at Glide.Tween.TweenerImpl.AddAndRemove() in D:\a\Blish-HUD\Blish-HUD\Blish HUD\Library\Glide\Tweener.cs:line 216```

## How to use
1. Add module to Blish HUD.
2. Enable module and open main window.
3. Just spam the "Refresh View" `Button` until Blish HUD crashes.

## Module setup explained

The "Refresh View" `Button` just resembles a quickly repeating asynchronous event.

When the button is clicked 2 things happen
1. The currently loaded `TweenUsingView` in the `ViewContainer` of the `WindowView` is unloaded
	- that causes the main `FlowPanel` being disposed.
	- that causes all the contained `TweenUsingControl`s to be disposed
	- that causes all the `Tween`s from those `Control`s being cancelled and completed
	- that causes those `Tween`s to be added to the `Tweener.toRemove` `ConcurrentQueue`
2. A new `TweenUsingView` is built and shown
	- that causes a new `FlowPanel` to be created with 100 `TweenUsingControl`s
	- that causes 1 new `Tween` being created for each `TweenUsingControl`
	- that causes each `Tween` to be added to the `Tweener.toAdd` `ConcurrentQueue`
	- that causes 100 calls to `Tweener.AddAndRemove()`

At the same time on the main thread
- `Tweener.Update(float secondsElapsed)` is called
- that causes
	1. all `Tween`s to be updated after one another 
	2. `Tweener.AddAndRemove()` to be called

It seems that somewhere in this interaction the `Exception`s are caused.