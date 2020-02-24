using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Models;
using System;
using Plugin.Media;
using System.Collections.Generic;
using InfiniteRechargeMetrics.Templates;

namespace InfiniteRechargeMetrics.ViewModels.StageVM.TemplateVM
{
    public class EditMatchRobotsViewModel : NotifyClass
    {
        public EditMatchRobotsTemplate EditMatchRobotsTemplate { get; set; }
        public bool ShouldFieldsReset { get; set; }
        public Match Match { get; set; }
        private string[] robotNameAndIds;
        public string[] RobotNameAndIds { 
            get => robotNameAndIds;
            set
            {
                robotNameAndIds = value;
                NotifyPropertyChanged();
            }
        }
        private byte displayRobotFrameAmount;
        public byte DisplayRobotFrameAmount
        {
            get => displayRobotFrameAmount;
            set
            {
                // The user could add or subtract past the limit
                if (value <= 6 && value >= 0)
                {
                    displayRobotFrameAmount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #region Robot Intermediate Props

        #region Robot Id Props
        public string RobotOneId { 
            get => Match.Robots[StageConstants.ROBOT_ONE_INDEX].RobotId;
            set
            {
                Match.Robots[StageConstants.ROBOT_ONE_INDEX].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotTwoId {
            get => Match.Robots[StageConstants.ROBOT_TWO_INDEX].RobotId;
            set
            {
                Match.Robots[StageConstants.ROBOT_TWO_INDEX].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotThreeId {
            get => Match.Robots[StageConstants.ROBOT_THREE_INDEX].RobotId;
            set
            {
                Match.Robots[StageConstants.ROBOT_THREE_INDEX].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFourId {
            get => Match.Robots[StageConstants.ROBOT_FOUR_INDEX].RobotId;
            set
            {
                Match.Robots[StageConstants.ROBOT_FOUR_INDEX].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFiveId {
            get => Match.Robots[StageConstants.ROBOT_FIVE_INDEX].RobotId;
            set
            {
                Match.Robots[StageConstants.ROBOT_FIVE_INDEX].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotSixId {
            get => Match.Robots[StageConstants.ROBOT_SIX_INDEX].RobotId;
            set
            {
                Match.Robots[StageConstants.ROBOT_SIX_INDEX].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ImagePath Props
        public string RobotOneImagePath { 
            get => Match.Robots[StageConstants.ROBOT_ONE_INDEX].ImagePath;
            set
            {
                Match.Robots[StageConstants.ROBOT_ONE_INDEX].ImagePath = value;
                NotifyPropertyChanged();
            }                
        }
        public string RobotTwoImagePath
        {
            get => Match.Robots[StageConstants.ROBOT_TWO_INDEX].ImagePath;
            set
            {
                Match.Robots[StageConstants.ROBOT_TWO_INDEX].ImagePath = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotThreeImagePath
        {
            get => Match.Robots[StageConstants.ROBOT_THREE_INDEX].ImagePath;
            set
            {
                Match.Robots[StageConstants.ROBOT_THREE_INDEX].ImagePath = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFourImagePath
        {
            get => Match.Robots[StageConstants.ROBOT_FOUR_INDEX].ImagePath;
            set
            {
                Match.Robots[StageConstants.ROBOT_FOUR_INDEX].ImagePath = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFiveImagePath
        {
            get => Match.Robots[StageConstants.ROBOT_FIVE_INDEX].ImagePath;
            set
            {
                Match.Robots[StageConstants.ROBOT_FIVE_INDEX].ImagePath = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotSixImagePath
        {
            get => Match.Robots[StageConstants.ROBOT_SIX_INDEX].ImagePath;
            set
            {
                Match.Robots[StageConstants.ROBOT_SIX_INDEX].ImagePath = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Robot Alias Props
        //public string RobotOneAlias { 
        //    get => Match.Robots[ROBOT_ONE_INDEX].RobotAlias;
        //    set
        //    {
        //        Match.Robots[ROBOT_ONE_INDEX].RobotAlias = value;
        //        NotifyPropertyChanged();
        //    } 
        //}
        //public string RobotTwoAlias
        //{
        //    get => Match.Robots[ROBOT_TWO_INDEX].RobotAlias;
        //    set
        //    {
        //        Match.Robots[ROBOT_TWO_INDEX].RobotAlias = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string RobotThreeAlias
        //{
        //    get => Match.Robots[ROBOT_THREE_INDEX].RobotAlias;
        //    set
        //    {
        //        Match.Robots[ROBOT_THREE_INDEX].RobotAlias = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string RobotFourAlias
        //{
        //    get => Match.Robots[ROBOT_FOUR_INDEX].RobotAlias;
        //    set
        //    {
        //        Match.Robots[ROBOT_FOUR_INDEX].RobotAlias = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string RobotFiveAlias
        //{
        //    get => Match.Robots[ROBOT_FIVE_INDEX].RobotAlias;
        //    set
        //    {
        //        Match.Robots[ROBOT_FIVE_INDEX].RobotAlias = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string RobotSixAlias
        //{
        //    get => Match.Robots[ROBOT_SIX_INDEX].RobotAlias;
        //    set
        //    {
        //        Match.Robots[ROBOT_SIX_INDEX].RobotAlias = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        #endregion

        #region Robot Comment Props
        public string RobotOneInfo { 
            get => Match.Robots[StageConstants.ROBOT_ONE_INDEX].RobotInfo;
            set
            {
                Match.Robots[StageConstants.ROBOT_ONE_INDEX].RobotInfo = value;
                NotifyPropertyChanged();
            } 
        }
        public string RobotTwoInfo
        {
            get => Match.Robots[StageConstants.ROBOT_TWO_INDEX].RobotInfo;
            set
            {
                Match.Robots[StageConstants.ROBOT_TWO_INDEX].RobotInfo = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotThreeInfo
        {
            get => Match.Robots[StageConstants.ROBOT_THREE_INDEX].RobotInfo;
            set
            {
                Match.Robots[StageConstants.ROBOT_THREE_INDEX].RobotInfo = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFourInfo
        {
            get => Match.Robots[StageConstants.ROBOT_FOUR_INDEX].RobotInfo;
            set
            {
                Match.Robots[StageConstants.ROBOT_FOUR_INDEX].RobotInfo = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFiveInfo
        {
            get => Match.Robots[StageConstants.ROBOT_FIVE_INDEX].RobotInfo;
            set
            {
                Match.Robots[StageConstants.ROBOT_FIVE_INDEX].RobotInfo = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotSixInfo
        {
            get => Match.Robots[StageConstants.ROBOT_SIX_INDEX].RobotInfo;
            set
            {
                Match.Robots[StageConstants.ROBOT_SIX_INDEX].RobotInfo = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #endregion

        private object robotOneSelected;
        public object RobotOneSelected
        {
            get => robotOneSelected;
            set
            {
                if (value != null)
                {
                    Match.Robots[StageConstants.ROBOT_ONE_INDEX].RobotId = ((string)value).Split(' ')[1];
                }
                robotOneSelected = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand GetPictureCMD { get; set; }
        public ICommand RevealARobotCMD { get; set; }
        public ICommand HideRobotCMD { get; set; }
        public ICommand ClearPickerCMD { get; set; }

        public EditMatchRobotsViewModel(EditMatchRobotsTemplate _editRobotTemplate, Match _match)
        {
            Match = _match;
            EditMatchRobotsTemplate = _editRobotTemplate;
            RevealARobotCMD = new Command(() => DisplayRobotFrameAmount++);
            HideRobotCMD = new Command(() => DisplayRobotFrameAmount--);
            ClearPickerCMD = new Command<string>(ClearPicker);
            GetPictureCMD = new Command(GetPicture);
            LoadRobotPicker();
        }

        private void ClearPicker(string _pickerNum)
        {
            if (_pickerNum == null)
                return;

            switch (int.Parse(_pickerNum))
            {
                case StageConstants.ROBOT_ONE_INDEX:
                    EditMatchRobotsTemplate.RobotOneIdPicker.SelectedItem = null;
                    break;
                case StageConstants.ROBOT_TWO_INDEX:
                    EditMatchRobotsTemplate.RobotTwoIdPicker.SelectedItem = null;
                    break;
                case StageConstants.ROBOT_THREE_INDEX:
                    EditMatchRobotsTemplate.RobotThreeIdPicker.SelectedItem = null;
                    break;
                case StageConstants.ROBOT_FOUR_INDEX:
                    EditMatchRobotsTemplate.RobotFourIdPicker.SelectedItem = null;
                    break;
                case StageConstants.ROBOT_FIVE_INDEX:
                    EditMatchRobotsTemplate.RobotFiveIdPicker.SelectedItem = null;
                    break;
                case StageConstants.ROBOT_SIX_INDEX:
                    EditMatchRobotsTemplate.RobotSixIdPicker.SelectedItem = null;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     Loads the robot pickers bind source with data
        /// </summary>
        private async void LoadRobotPicker()
        {
            RobotNameAndIds = await Data.DatabaseService.Provider.GetAllRobotIdAsync();
        }

        /// <summary>
        ///     Validates that the user can store an image at runtime and generates required UI.
        ///     Retreives that picture using CrossMedia and saves the path into the corresponding robot's 
        ///         ImagePath property.
        /// </summary>
        /// <param name="_index"> Index passed to know which robot the request was called upon </param>
        private async void GetPicture(object _index)
        {
            // Makes onappearing not reset all the ui fields
            ShouldFieldsReset = false;
            try
            {
                int index = int.Parse((string)_index);

                if (string.IsNullOrEmpty(Match.Robots[index].RobotId))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "In order to save a picture of the robot you must enter a valid Id first (New or Existing).", "OK");
                    return;
                }

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    // We dont need a massive image when inside the app.. make this changable at some point
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    Directory = "Robots",
                    Name = $"{Match.Robots[index].RobotId}.jpg"
                });

                if (file == null)
                    return;

                switch (index)
                {
                    case StageConstants.ROBOT_ONE_INDEX:
                        RobotOneImagePath = file.Path;
                        break;
                    case StageConstants.ROBOT_TWO_INDEX:
                        RobotTwoImagePath = file.Path;
                        break;
                    case StageConstants.ROBOT_THREE_INDEX:
                        RobotThreeImagePath = file.Path;
                        break;
                    case StageConstants.ROBOT_FOUR_INDEX:
                        RobotFourImagePath = file.Path;
                        break;
                    case StageConstants.ROBOT_FIVE_INDEX:
                        RobotFiveImagePath = file.Path;
                        break;
                    case StageConstants.ROBOT_SIX_INDEX:
                        RobotSixImagePath = file.Path;
                        break;

                    default:
                        break;
                }

                await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
            }
            catch { }
            
                            
            //meow.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});
        }
    }
}
