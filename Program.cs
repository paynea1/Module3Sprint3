using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;
using System.Text.RegularExpressions;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Solution with the helper methods,
    //              opening and closing screens, and the menu. Sprint 3
    // Application Type: Console
    // Author: Payne, Austin
    // Dated Created: 2/19/2020
    // Last Modified: 2/27/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming - UNAVAILABLE");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DisplayDataRecorderMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmDisplayMenuScreen(finchRobot);
                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }



        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Square dance");
                Console.WriteLine("\tc) Lights, sound, and dancing");
                Console.WriteLine("\td) ");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(myFinch);
                        break;

                    case "b":
                        DisplayDance(myFinch);
                        break;

                    case "c":
                        DisplayMixingItUp(myFinch);
                        break;

                    case "d":

                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        static void DisplayDance(Finch finchRobot)
        {
            for (int times = 0; times < 4; times++)
            {
                finchRobot.setMotors(255, 255);
                finchRobot.setMotors(255, 0);

            }


        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = true;

            DisplayScreenHeader("Light and Sound (modified)");

            Console.WriteLine("\tThe Finch robot will show off its glowing talent!");
            bool validResponse;
            int red;
            int green;
            int blue;

            do
            {
                validResponse = true;
                Console.Write(" Enter a starting color (red, green, or blue:");
                string userResponse = Console.ReadLine();
                if (userResponse == "red")
                {
                    validResponse = true;
                    for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
                    {
                        finchRobot.setLED(lightSoundLevel, 0, 0);
                        finchRobot.noteOn(lightSoundLevel * 99);
                    }
                }
                else if (userResponse == "green")
                {
                    validResponse = true;
                    for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
                    {
                        finchRobot.setLED(0, lightSoundLevel, 0);
                        finchRobot.noteOn(lightSoundLevel * 99);
                    }
                }
                else if (userResponse == "blue")
                {
                    validResponse = true;
                    for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
                    {
                        finchRobot.setLED(0, 0, lightSoundLevel);
                        finchRobot.noteOn(lightSoundLevel * 99);
                    }
                }
            } while (validResponse != true);


            DisplayContinuePrompt();

            DisplayMenuPrompt("Talent Show Menu");
        }

        static void DisplayMixingItUp(Finch myFinch)
        {
            DisplayLightAndSound(myFinch);
            DisplayDance(myFinch);
        }
        #endregion

        #region DATA RECORDER

        private static void DisplayDataRecorderMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;
            bool quitToMenu = false;
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0.0;
            double[] temperatures = (double[])null;
            do
            {
                DisplayScreenHeader("Data Recorder Menu");
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");

                //
                //Process the user choice
                //
                switch (Console.ReadLine().ToLower())
                {
                    case "a":
                        numberOfDataPoints = DisplayGetNumberOfDataPoints();
                        break;
                    case "b":
                        dataPointFrequency = DisplayGetDataPointFrequency();
                        break;
                    case "c":
                        temperatures = DisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;
                    case "d":
                        DisplayData(temperatures);
                        break;
                    case "q":
                        quitToMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            }
            while (!quitToMenu);
        }

        static bool GetValidInteger(
      string prompt,
      int minValue,
      int maxValue,
      out int validInteger)
        {
            bool flag1 = false;
            validInteger = 0;
            bool flag2 = minValue != 0 || (uint)maxValue > 0;
            Console.Write(prompt);
            while (!flag1)
            {
                if (int.TryParse(Console.ReadLine(), out validInteger))
                {
                    if (flag2)
                    {
                        if (validInteger >= minValue && validInteger <= maxValue)
                        {
                            flag1 = true;
                        }
                        else
                        {
                            Console.WriteLine(string.Format($"\tYou must enter an integer value between {minValue} and {maxValue}. You must"));
                            Console.WriteLine();
                            Console.Write(prompt);
                        }
                    }
                    else
                        flag1 = true;
                }
                else
                {
                    Console.WriteLine("\tYou must enter an integer value. Please try again.");
                    Console.WriteLine();
                    Console.Write(prompt);
                }
            }
            Console.CursorVisible = false;
            return true;
        }

        static double DisplayGetDataPointFrequency()
        {
            bool validResponse;
            double frequency;
            DisplayScreenHeader("Get Data Point Frequency");
            do
            {
                Console.WriteLine("How frquent (in seconds): ");
                string userResponse = Console.ReadLine();
                validResponse = double.TryParse(userResponse, out frequency);
            } while (validResponse != true);
            DisplayContinuePrompt();
            return frequency;
        }

        private static void DisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Data");
            DisplayDataTable(temperatures);
            DisplayMenuPrompt("Data Recorder");

        }
        private static void DisplayDataTable(double[] temperatures)
        {
            DisplayScreenHeader("Temperatures");
            Console.WriteLine("\tReading Number: " + "Temp");
            Console.WriteLine("\t****************************");
            for (int index = 0; index < temperatures.Length; ++index)
                Console.WriteLine(string.Format("\t{0}", (object)(index + 1)) + temperatures[index].ToString("n2"));
        }

        private static double[] DisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] numArray = new double[numberOfDataPoints];
            DisplayScreenHeader("Get Temperatures");
            Console.WriteLine(string.Format("\tData Points: {0}", (object)numberOfDataPoints));
            Console.WriteLine(string.Format("\tFrequency: {0}", (object)dataPointFrequency));
            Console.WriteLine();
            Console.WriteLine("\tThe Finch robot is ready to gather the temperature data.");
            DisplayContinuePrompt();
            Console.WriteLine("\tTemp");
            Console.WriteLine("\t*********************");
            for (int index = 0; index < numberOfDataPoints; ++index)
            {
                numArray[index] = finchRobot.getTemperature();
                int ms = (int)(dataPointFrequency * 1000.0);
                finchRobot.wait(ms);
                Console.WriteLine(string.Format("\t{0}", (object)(index + 1)) + numArray[index].ToString("n2"));
            }
            DisplayMenuPrompt("Data Recorder");
            return numArray;
        }

        private static int DisplayGetNumberOfDataPoints()
        {
            DisplayScreenHeader("Number of Data Points");
            int validInteger;
            GetValidInteger("\tNumber of Data Points: ", 1, 100, out validInteger);
            DisplayMenuPrompt("Data Recorder");
            return validInteger;
        }



        #endregion

        #region ALARM SYSTEM

        static void LightAlarmDisplayMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitAlarmSystemMenu = false;
            string menuChoice;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;


            do
            {
                DisplayScreenHeader("Alarm System Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum or Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmSetMinMaxThresholdValue(rangeType, myFinch);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmSetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(myFinch, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;
                    case "q":
                        quitAlarmSystemMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitAlarmSystemMenu);
        }

        static void LightAlarmSetAlarm(
            Finch myFinch, 
            string sensorsToMonitor, 
            string rangeType, 
            int minMaxThresholdValue, 
            int timeToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;

            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"Sensors to monitor: {sensorsToMonitor}");
            Console.WriteLine($"Range Type: {rangeType}");
            Console.WriteLine($"Min/Max Value: {minMaxThresholdValue}");
            Console.WriteLine($"Time to monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring...");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < timeToMonitor) && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = myFinch.getLeftLightSensor();
                        break;

                    case "right":
                        currentLightSensorValue = myFinch.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (myFinch.getRightLightSensor() + myFinch.getLeftLightSensor()) / 2;
                        break;
                }


                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                secondsElapsed++;

                if (thresholdExceeded)
                {
                    Console.WriteLine($"The {rangeType} threshold value of {minMaxThresholdValue} has been exceeded by the current light sensor value of {currentLightSensorValue}");
                }
                else
                {
                    Console.WriteLine($"The {rangeType} threshold value has not been exceeded.");
                }
                myFinch.wait(1000);
                secondsElapsed++;
            }
            DisplayMenuPrompt("Light Alarm System");
        }

        static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch myFinch)
        {
            int minMaxThresholdValue = 0;
            bool validResponse;
            string userResponse;

            do
            {
                DisplayScreenHeader("Minimum/Maximum Threshold Value");

                Console.WriteLine($"Left light sensor ambient light level: {myFinch.getLeftLightSensor()}");
                Console.WriteLine($"Right light sensor ambient light level: {myFinch.getRightLightSensor()}");
                Console.WriteLine();
                Console.Write($"Enter the {rangeType} light sensor value:");
                userResponse = Console.ReadLine();
                
                if (Regex.IsMatch(userResponse, @"^\d+$")) //A method I stole from https://www.techiedelight.com/identify-string-is-numeric-csharp/
                {
                    validResponse = true;
                    int.TryParse(userResponse, out minMaxThresholdValue);
                }
                else
                {
                    validResponse = false;
                    Console.WriteLine("Hmm.. That didn't work. Press any key to try again");
                    Console.ReadKey();
                }
            } while (!validResponse);

            Console.WriteLine($"The {rangeType} value is set to {minMaxThresholdValue}");
            

            DisplayMenuPrompt("Light Alarm");
            return minMaxThresholdValue;


        }



        static string LightAlarmDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor;
            bool validResponse;

            DisplayScreenHeader("Sensors to Monitor");

            do
            {

                Console.Write("\tWhich sensors are we to monitor? [left, right, both]: ");
                sensorsToMonitor = Console.ReadLine();
                if (sensorsToMonitor == "left" || sensorsToMonitor == "right" || sensorsToMonitor == "both")
                {
                    validResponse = true;
                }
                else
                {
                    validResponse = false;
                    Console.WriteLine("Hmm.. That didn't work. Press any key to try again");
                    Console.ReadKey();
                }
            } while (validResponse == false);

            switch (sensorsToMonitor)
            {
                case "left":
                    Console.WriteLine("Okay, I will monitor the left light sensor for you.");
                    break;

                case "right":
                    Console.WriteLine("Alright, I will monitor the right light sensor for you.");
                    break;
                case "both":
                    Console.WriteLine("I see. I will monitor both light sensors for you.");
                    break;

            }


            DisplayMenuPrompt("Light Alarm");

            return sensorsToMonitor;
        }

        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType;
            bool validResponse;

            DisplayScreenHeader("Range Type");
            do
            {
                Console.Write("\tRange Type [minimum, maximum]:");
                rangeType = Console.ReadLine();
                if (rangeType == "minimum" || rangeType == "maximum")
                {
                    validResponse = true;
                }
                else
                {
                    validResponse = false;
                    Console.WriteLine("Hmm.. That didn't work. Press any key to try again");
                    Console.ReadKey();
                }
            } while (!validResponse);

            Console.WriteLine($"The range type is set to {rangeType}");

            DisplayMenuPrompt("Light Alarm");

            return rangeType;
        }

        static int LightAlarmSetTimeToMonitor()
        {
            int timeToMonitor = 0;
            bool validResponse;
            string userResponse;
            DisplayScreenHeader("Time to Monitor");


            do
            {
                Console.Write($"\tTime to Monitor: ");
                userResponse = Console.ReadLine();
                if (Regex.IsMatch(userResponse, @"^\d+$"))
                {
                    validResponse = true;
                    int.TryParse(userResponse, out timeToMonitor);
                }
                else
                {
                    validResponse = false;
                    Console.WriteLine("Hmm.. That didn't work. Press any key to try again");
                    Console.ReadKey();
                }
            } while (!validResponse);

            Console.WriteLine($"The time to monitor is set to {timeToMonitor}");
            DisplayMenuPrompt("Light Alarm");
            return timeToMonitor;


        }
        #endregion

        #region USER PROGRAMMING

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
