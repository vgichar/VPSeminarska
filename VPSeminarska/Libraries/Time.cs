using System;

// basic time managing class used for ensuring frame independence
// @startTime and @endTime are auxiliary variables only used in the @MainWindow game loop to calculate the @deltaTime value
// @deltaTime has a very specific purpose, to make every movement frame independent
//
// e.g.
// we have a circle
// @Circle.Center = new Point(0, 0); - rendering pivot
// @Circle.MoveDirection = new Vector2D(1, 0);
// @Circle.Speed = 50; - 50 pixels per second
//
// public void OnPaint(){
//      @Circle.Center += @Circle.MoveDirection * Time.deltatime;
// }
// 
// if Time.deltaTime == 1 -- 1 frame == 1 second, the circle will move full @MoveDirection amount
// if Time.deltaTime == 0.33 -- 1 frame == 0.33 second, the circle will move 0.33 * @MoveDirection amount,
// but in 1 second there will be 3(approximately) frames executed, so in 1 second the @Circle will move full @MoveDirection pixels
// independent of the frame rate (frames per second)

namespace VPSeminarska.Libraries
{
    public static class Time
    {
        public static double deltaTime = 1;
        public static double startTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
        public static double endTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
    }
}
