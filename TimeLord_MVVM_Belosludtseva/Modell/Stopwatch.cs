using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TimeLord_MVVM_Belosludtseva.ViewModell;

namespace TimeLord_MVVM_Belosludtseva.Modell
{
    public class Stopwatch : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string Timer
        {
            get
            {
                float Hour = (Time / 60f / 60f);
                float Minute = (Time / 60f) - ((int)Hour * 60f);
                float Second = Time - (int)Hour * 60f * 60f - (int)Minute * 60f;
                string sHour = ((int)Hour).ToString();
                string sMinute = ((int)Minute).ToString();
                string sSecond = ((int)Second).ToString();
                if (Hour < 10) sHour = "0" + ((int)Hour).ToString();
                if (Minute < 10) sMinute = "0" + ((int)Minute).ToString();
                if (Second < 10) sSecond = "0" + ((int)Second).ToString();
                return $"{sHour}:{sMinute}:{sSecond}";
            }
        }

        private int time;

        public int Time
        {
            get {  return time; }

            set
            {
                time = value;
                OnPropertyChanged("Timer");
            }
        }

        public bool Work;
        private string textButton = "Начать";
        public string TextButton
        {
            get { return textButton; }
            set
            {
                textButton = value;
                OnPropertyChanged("TextButton");
            }
        }
        public ObservableCollection<string> Interval { get; set; }
        public Stopwatch()
        {
            Interval = new ObservableCollection<string>();
        }

        private RelayCommand startTimer;
        public RelayCommand StartTimer
        {
            get
            {
                return startTimer ??

                    (startTimer = new RelayCommand(obj =>
                    {
                        if (Work == false)
                        {
                            Interval.Clear();
                            Time = 0;
                            Work = true;
                            TextButton = "Стоп";
                        }
                        else
                        {
                            Work = false;
                            TextButton = "Начать";
                        }
                    }));
            }
        }
        private RelayCommand intervalTimer;
        public RelayCommand IntervalTimer
        {
            get
            {
                return intervalTimer ??

                    (intervalTimer = new RelayCommand(obj =>
                    {
                        if (Work)
                        {
                            Interval.Insert(0, Timer);
                        }
                    }));
            }
        }
    }
}
