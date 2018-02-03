using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
namespace LeapMotionTest
{
    class Program
    {
        class SampleListener
        {
            public void OnServiceConnect(object sender, ConnectionEventArgs args)
            {
                Console.WriteLine("Service Connected");
            }

            public void OnConnect(object sender, DeviceEventArgs args)
            {
                Console.WriteLine("Connected");
            }

            public void OnFrame(object sender, FrameEventArgs args)
            {
                // Get the most recent frame and report some basic information
                Frame frame = args.frame;

                Console.WriteLine(
                  "Frame id: {0}, timestamp: {1}, hands: {2}",
                  frame.Id, frame.Timestamp, frame.Hands.Count
                );
                foreach (Hand hand in frame.Hands)
                {
                    Console.WriteLine("  Hand id: {0}, palm position: {1}, fingers: {2}",
                      hand.Id, hand.PalmPosition, hand.Fingers.Count);
                    // Get the hand's normal vector and direction
                    Vector normal = hand.PalmNormal;
                    Vector direction = hand.Direction;

                    // Calculate the hand's pitch, roll, and yaw angles
                    Console.WriteLine(
                      "  Hand pitch: {0} degrees, roll: {1} degrees, yaw: {2} degrees",
                      direction.Pitch * 180.0f / (float)Math.PI,
                      normal.Roll * 180.0f / (float)Math.PI,
                      direction.Yaw * 180.0f / (float)Math.PI
                    );
                }
            }

        }
        public static void Main()
        {
            Controller controller = new Controller();
            SampleListener listener = new SampleListener();
            controller.Connect += listener.OnServiceConnect;
            controller.Device += listener.OnConnect;
            controller.FrameReady += listener.OnFrame;

            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();

           
        }
    }
}
