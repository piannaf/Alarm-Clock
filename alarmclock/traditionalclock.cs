//http://www.codeproject.com/KB/WPF/WPFParticleEffects.aspx
using System;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

//http://msdn.microsoft.com/en-us/library/system.windows.controls.image.source.aspx
using System.Windows.Media.Imaging;


namespace Piannaf.Ports.Microsoft.Samples.WinFX.AlarmClock{
    public class TraditionalClock : Window{
        Canvas clockCanvas = new Canvas();

        public TraditionalClock(){
            //http://msdn.microsoft.com/en-us/library/system.windows.media.animation.storyboard.aspx
            // Create a name scope for the window.
            NameScope.SetNameScope(this, new NameScope());

            this.Name = "clockWindow";
            this.AllowsTransparency=true;
            this.Background=Brushes.Transparent;
            this.WindowStyle=WindowStyle.None;
            this.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LeftButtonDown);

            //this.RegisterName(this.Name, this);

            Storyboard clockHandStoryboard = new Storyboard();
            
            //// create the parallel timeline which contains the clockhand animations
            ParallelTimeline pt = new ParallelTimeline(TimeSpan.FromSeconds(0));
            
            //<DoubleAnimation From="-9" To="351" Duration="00:01:00" RepeatBehavior="Forever" 
            //                    Storyboard.TargetProperty="Angle" Storyboard.TargetName="secondHandAngle"/>
            DoubleAnimation secondHandAnimation = new DoubleAnimation(-9, 351, new Duration(new TimeSpan(0, 1, 0)));
            Storyboard.SetTargetName(secondHandAnimation, "secondHandAngle");
            Storyboard.SetTargetProperty(secondHandAnimation, new PropertyPath(RotateTransform.AngleProperty));
            secondHandAnimation.RepeatBehavior = RepeatBehavior.Forever;

            //<DoubleAnimation From="-9" To="351" Duration="12:00:00" RepeatBehavior="Forever" 
            //                    Storyboard.TargetProperty="Angle" Storyboard.TargetName="hourHandAngle" />
            DoubleAnimation hourHandAngleAnimation = new DoubleAnimation(-9, 351, new Duration(new TimeSpan(12, 0, 0)));
            Storyboard.SetTargetName(hourHandAngleAnimation, "hourHandAngle");
            Storyboard.SetTargetProperty(hourHandAngleAnimation, new PropertyPath(RotateTransform.AngleProperty));
            hourHandAngleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            //<DoubleAnimation From="-9" To="351" Duration="01:00:00" RepeatBehavior="Forever"  
            //                    Storyboard.TargetProperty="Angle" Storyboard.TargetName="minuteHandAnimation" />
            DoubleAnimation minuteHandAnimation = new DoubleAnimation(-9, 351, new Duration(new TimeSpan(1, 0, 0)));
            Storyboard.SetTargetName(minuteHandAnimation, "minuteHandAngle");
            Storyboard.SetTargetProperty(minuteHandAnimation, new PropertyPath(RotateTransform.AngleProperty));
            minuteHandAnimation.RepeatBehavior = RepeatBehavior.Forever;

            //// add the animations to the timeline
            pt.Children.Add(secondHandAnimation);
            pt.Children.Add(hourHandAngleAnimation);
            pt.Children.Add(minuteHandAnimation);

            //// add the timeline to the storyboard
            clockHandStoryboard.Children.Add(pt);

            this.Resources.Add("clockHandStoryboard", clockHandStoryboard);
            this.RegisterName("clockHandStoryboard", clockHandStoryboard);
            
            ////<Canvas Name="clockCanvas" Width="292" Height="493"  >
            clockCanvas.Name = "clockCanvas";
            clockCanvas.Width = 292;
            clockCanvas.Height = 493;

            //<EventTrigger RoutedEvent="Canvas.Loaded">
            EventTrigger canvasTrigger = new EventTrigger();
            canvasTrigger.RoutedEvent = Canvas.LoadedEvent;

            //<BeginStoryboard Name ="clockHandStoryboard" Storyboard="{StaticResource clockHandStoryboard}" />
            BeginStoryboard beginStory = new BeginStoryboard();
            beginStory.Name = "clockHandStoryboard";
            beginStory.Storyboard = clockHandStoryboard;

            canvasTrigger.Actions.Add(beginStory);
            clockCanvas.Triggers.Add(canvasTrigger);

            

            /** Clock Hands */
            //<Image Source="TradClock.png" Loaded="SetTime" />
            Image tradClock = new Image(); 
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("TradClock.png", UriKind.Relative);
            bi3.EndInit();
            tradClock.Source = bi3;
            tradClock.Loaded += new RoutedEventHandler(this.SetTime);
            clockCanvas.Children.Add(tradClock);

            //<Polygon Name="hourHand" Canvas.Top="214" Canvas.Left="173"	Points="0,5 3,0 4,0 8,5 8,50 0,50">
            Polygon hourHand = new Polygon();
            //http://msdn.microsoft.com/en-us/library/system.windows.shapes.polygon.points.aspx
            Point hPoint1 = new Point(0, 5);
            Point hPoint2 = new Point(3, 0);
            Point hPoint3 = new Point(4, 0);
            Point hPoint4 = new Point(8, 5);
            Point hPoint5 = new Point(8, 50);
            Point hPoint6 = new Point(0, 50);
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(hPoint1);
            myPointCollection.Add(hPoint2);
            myPointCollection.Add(hPoint3);
            myPointCollection.Add(hPoint4);
            myPointCollection.Add(hPoint5);
            myPointCollection.Add(hPoint6);
            hourHand.Points = myPointCollection;
            //<LinearGradientBrush StartPoint="0,0" EndPoint="1,0">
            //http://msdn.microsoft.com/en-us/library/system.windows.shapes.shape.fill.aspx
            LinearGradientBrush hourHandBrush = new LinearGradientBrush();
            hourHandBrush.StartPoint = new Point(0, 0);
            hourHandBrush.EndPoint = new Point(1, 0);
            //<LinearGradientBrush.GradientStops>
            //<GradientStop Offset="0" Color="White" />
            //<GradientStop Offset="1" Color="DarkGray" />
            //http://msdn.microsoft.com/en-us/library/system.windows.media.lineargradientbrush.aspx
            hourHandBrush.GradientStops.Add(new GradientStop(Colors.White, 0));
            hourHandBrush.GradientStops.Add(new GradientStop(Colors.DarkGray, 1));
            hourHand.Fill = hourHandBrush;
            hourHand.Name = "hourHand";
            //<Polygon.RenderTransform>
            //<RotateTransform x:Name="hourHandAngle" CenterX="4" CenterY="45" />
            //http://msdn.microsoft.com/en-us/library/ms752357.aspx
            RotateTransform hourHandAngle = new RotateTransform();
            this.RegisterName("hourHandAngle", hourHandAngle);
            hourHandAngle.CenterX = 4;
            hourHandAngle.CenterY = 45;
            hourHand.RenderTransform = hourHandAngle;
            //http://msdn.microsoft.com/en-us/library/system.windows.controls.canvas.top.aspx
            Canvas.SetTop(hourHand, 214);
            Canvas.SetLeft(hourHand, 173);
            clockCanvas.Children.Add(hourHand);

            //<Polygon Name="minuteHand" Canvas.Top="183" Canvas.Left="173"	Points="0,5 1,0 2,0 4,5 4,80 0,80">
            Polygon minuteHand = new Polygon();
            Point mPoint1 = new Point(0, 5);
            Point mPoint2 = new Point(1, 0);
            Point mPoint3 = new Point(2, 0);
            Point mPoint4 = new Point(4, 5);
            Point mPoint5 = new Point(4, 80);
            Point mPoint6 = new Point(0, 80);
            PointCollection myPointCollection2 = new PointCollection();
            myPointCollection2.Add(mPoint1);
            myPointCollection2.Add(mPoint2);
            myPointCollection2.Add(mPoint3);
            myPointCollection2.Add(mPoint4);
            myPointCollection2.Add(mPoint5);
            myPointCollection2.Add(mPoint6);
            minuteHand.Points = myPointCollection2;
            minuteHand.Fill = hourHandBrush;
            minuteHand.Name = "minuteHand";
            //<RotateTransform x:Name="minuteHandAnimation" CenterX="2" CenterY="75"/>
            RotateTransform minuteHandAngle = new RotateTransform();
            this.RegisterName("minuteHandAngle", minuteHandAngle);
            minuteHandAngle.CenterX = 2;
            minuteHandAngle.CenterY = 75;
            minuteHand.RenderTransform = minuteHandAngle;

            Canvas.SetTop(minuteHand, 183);
            Canvas.SetLeft(minuteHand, 173);
            clockCanvas.Children.Add(minuteHand);

            //<Polygon Name="secondHand" Canvas.Top="170" Canvas.Left="175" Points="0,0 2,0 2,95 0,95" >
            Polygon secondHand = new Polygon();
            Point sPoint1 = new Point(0, 0);
            Point sPoint2 = new Point(2, 0);
            Point sPoint3 = new Point(2, 95);
            Point sPoint4 = new Point(0, 95);
            PointCollection myPointCollection3 = new PointCollection();
            myPointCollection3.Add(sPoint1);
            myPointCollection3.Add(sPoint2);
            myPointCollection3.Add(sPoint3);
            myPointCollection3.Add(sPoint4);
            secondHand.Points = myPointCollection3;
            secondHand.Fill = hourHandBrush;
            secondHand.Name = "minuteHand";
            //<RotateTransform x:Name="secondHandAngle" CenterX="0" CenterY="90"/>
            RotateTransform secondHandAngle = new RotateTransform();
            this.RegisterName("secondHandAngle", secondHandAngle);
            secondHandAngle.CenterX = 0;
            secondHandAngle.CenterY = 90;
            secondHand.RenderTransform = secondHandAngle;

            Canvas.SetTop(secondHand, 170);
            Canvas.SetLeft(secondHand, 175);
            clockCanvas.Children.Add(secondHand);

            //<!--Center circles-->
            //<Ellipse Canvas.Top="248" Canvas.Left="168" Width="18" Height="20" Stroke="DarkGray">
            Ellipse outerEllipse = new Ellipse();
            Canvas.SetTop(outerEllipse, 248);
            Canvas.SetLeft(outerEllipse, 168);
            outerEllipse.Width = 18;
            outerEllipse.Height = 20;
            outerEllipse.Stroke = Brushes.DarkGray;
            //<LinearGradientBrush StartPoint="0,0" EndPoint="0,1">
            LinearGradientBrush outerEllipseBrush = new LinearGradientBrush();
            outerEllipseBrush.StartPoint = new Point(0, 0);
            outerEllipseBrush.EndPoint = new Point(1, 0);
            //<LinearGradientBrush.GradientStops>
            //<GradientStop Offset="0" Color="LightCoral"/>
            //<GradientStop Offset="1" Color="Red"/>
            outerEllipseBrush.GradientStops.Add(new GradientStop(Colors.LightCoral, 0));
            outerEllipseBrush.GradientStops.Add(new GradientStop(Colors.Red, 1));
            outerEllipse.Fill = outerEllipseBrush;
            clockCanvas.Children.Add(outerEllipse);

            //<Ellipse Canvas.Top="254" Canvas.Left="174" Width="6" Height="8" Fill="DarkGray" Stroke="Black"  />
            Ellipse innerEllipse = new Ellipse();
            Canvas.SetTop(innerEllipse, 254);
            Canvas.SetLeft(innerEllipse, 174);
            innerEllipse.Width = 6;
            innerEllipse.Height = 8;
            innerEllipse.Fill = Brushes.DarkGray;
            innerEllipse.Stroke = Brushes.Black;
            clockCanvas.Children.Add(innerEllipse);

            this.Content = clockCanvas;
        }

        // Set the time to the current time - note that this must be triggered 
        // AFTER the animations have actually been started - in this case, after
        // the Canvas has loaded
        private void SetTime(object sender, RoutedEventArgs e)
        {
            // Fetch the storyboard and advance time 
            Storyboard clockHandStoryboard = (Storyboard)this.Resources["clockHandStoryboard"];
            clockHandStoryboard.Seek(clockCanvas, DateTime.Now.TimeOfDay, TimeSeekOrigin.BeginTime);   
        }

        private void LeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}