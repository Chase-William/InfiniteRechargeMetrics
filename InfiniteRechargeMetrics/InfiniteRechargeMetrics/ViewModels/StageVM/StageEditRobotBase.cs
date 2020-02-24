using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class StageEditRobotBase : NotifyClass
    {
        public Match Match { get; set; }

        public StageEditRobotBase(Match _match)
        {
            Match = _match;
        }

        /// <summary>
        ///     Validates the information provided by the user for the robots
        /// </summary>
        protected bool ValidateRobotsInputs()
        {
            // If a robot has data put into its fields and doesnt have a pk set warn the user
            if (Match.Robots.Any(robot => string.IsNullOrEmpty(robot.RobotId) && !string.IsNullOrEmpty(robot.RobotInfo)))
            {
                App.Current.MainPage.DisplayAlert("Warning", "You have put information into a robot entry and not given it an id. The robot will not be saved.", "OK");
            }          

            // checking each robot to make sure another robots hasnt been given the same id
            for (int i = 0; i < Match.Robots.Length; i++)
            {
                for (int y = 0; y < Match.Robots.Length; y++)
                {
                    // We dont wanna compare itself to itself...
                    if (i != y)
                    {
                        if (Match.Robots[i].RobotId == Match.Robots[y].RobotId)
                        {
                            // the robots cant have the same id unless they are both null or empty string
                            if (string.IsNullOrEmpty(Match.Robots[i].RobotId) && string.IsNullOrEmpty(Match.Robots[y].RobotId))
                            {
                                break;
                            }
                            else
                            {
                                App.Current.MainPage.DisplayAlert("Error", "Two robots you have provided an id for are the same.", "OK");
                                return false;
                            }
                        }                      
                    }
                }
            }

            foreach (var robot in Match.Robots)
            {
                if (!string.IsNullOrEmpty(robot.RobotId) && string.IsNullOrEmpty(robot.ImagePath))
                {
                    robot.ImagePath = "default_robot_icon.png";
                }
            }

            //if (Match.Robots.Any(robot => Match.Robots.Select(x => x.RobotId).Contains(robot.RobotId)))
            //{
            //    App.Current.MainPage.DisplayAlert("Error", "Two robots you have provided an id for are the same.", "OK");
            //    return false;
            //}

            return true;
        }
    }
}
