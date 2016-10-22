namespace TelegramGameBot
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class Program
    {
        static void Main(string[] args)
        {
            Run(500, 405, 525, 530, 75, "https://tbot.xyz/lumber/#eyJ1IjoyODEwMTIsIm4iOiJBbGJlcnQgWmFraWV2IiwiZyI6Ikx1bWJlckphY2siLCJjaSI6Ii05MTc0NzA1OTM2NjkwNDg2MTAwIiwiaSI6IkFnQUFBSHRiQUFDMFNRUUFMZHVXYmJCNHdhVSJ9OTc5ZTc5YzFmMGYzZjM3YmYwM2I5ZDkyYjRjODEyYWQ=&tgShareScoreUrl=tg%3A%2F%2Fshare_game_score%3Fhash%3DSxaGKGpDGLpZAGCWDyrFmVszExxJcgLmFhRMuhepOXA");
        }

        public static void Run(int iterations, int leftX, int rightX, int y, int checkLength, string url)
        {
            IWebDriver webDriver = new ChromeDriver();
            try
            {
                webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(10));
                webDriver.Navigate().GoToUrl(url);

                Thread.Sleep(1000);

                IWebElement leftButton = webDriver.FindElement(By.Id("button_left"));
                leftButton.Click();

                Thread.Sleep(1000);

                IWebElement rightButton = webDriver.FindElement(By.Id("button_right"));

                bool isLeft = false;

                for (int i = 0; i < iterations; i++)
                {
                    if (isLeft)
                    {
                        leftButton.Click();
                    }
                    else
                    {
                        rightButton.Click();
                    }

                    Thread.Sleep(20);

                    using (MemoryStream memoryStream = new MemoryStream(((ITakesScreenshot)webDriver).GetScreenshot().AsByteArray))
                    {
                        using (Bitmap bitmap = new Bitmap(memoryStream))
                        {
                            if (NeedChangePosition(bitmap, isLeft ? leftX : rightX, y, checkLength))
                            {
                                isLeft = !isLeft;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.Write(e);
            }
            finally
            {
                Console.ReadKey();
                webDriver.Dispose();
            }
        }

        private static bool NeedChangePosition(Bitmap bitmap, int x, int y, int length)
        {
            for (int i = y - length; i < y; i++)
            {
                if (bitmap.GetPixel(x, i).B < 100)
                {
                    return true;
                }
            }

            return false;
        }
    }
}