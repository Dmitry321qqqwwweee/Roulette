using System.Collections;
using UnityEngine;
using System;

public class TimeBonus : MonoBehaviour
{
    private static Bonus dailyBonus = new Bonus(Type.DailyBonus, 24f);

    public enum Type
    {
        DailyBonus
    }

    public static Bonus Get(Type _type)
    {
        switch (_type)
        {
            case Type.DailyBonus:
                return dailyBonus;
            default:
                return null;
        }
    }

    public class Bonus
    {
        public Action<bool> OnSetAvailible = (bool _isAvailible) => { };
        public Action OnSetTimeRemain = () => { };

        private string NAME = default;
        private float period = default;

        public bool IsAvailible
        {
            get => DateTime.Now > lastOpening.AddHours(period);
            private set
            {
                lastOpening = DateTime.Now.AddHours((value ? -1 : 0) * period);
                var timeRemain = lastOpening.AddHours(period) - DateTime.Now;
                    seconds = timeRemain.Seconds;
                    minutes = timeRemain.Minutes;
                    hours = timeRemain.Hours;
                    days = timeRemain.Days;
            }
        }

        private const string LAST_OPENING = "LastOpening";
        private DateTime lastOpening
        {
            get
            {
                if (!PlayerPrefs.HasKey(NAME + LAST_OPENING)) { lastOpening = DateTime.Now.AddHours(-period); }
                return String_To_DateTime(PlayerPrefs.GetString(NAME + LAST_OPENING));
            }
            set
            {
                PlayerPrefs.SetString(NAME + LAST_OPENING, DateTime_To_String(value));
                OnSetAvailible(IsAvailible);
            }
        }

        private int days = default;
        private int hours = default;
        private int minutes = default;
        private int seconds = default;

        public Bonus(Type _name, float _period)
        {
            NAME = _name + "";
            period = _period;
        }

        public void Init()
        {
            var timeRemain = lastOpening.AddHours(period) - DateTime.Now;
            seconds = timeRemain.Seconds;
            minutes = timeRemain.Minutes;
            hours = timeRemain.Hours;
            days = timeRemain.Days;
        }

        public void Get()
        {
            IsAvailible = false;
        }

        public string GetRest(string _format = "HMS", string _separator = ":")
        {
            string GetNumber(int _number) =>
                (_number > 9 ? "" : "0") + _number;

            string result = "";
            foreach (var ch in _format)
            {
                switch (ch)
                {
                    case 'D':
                        result += GetNumber(days) + _separator;
                        break;
                    case 'H':
                        result += GetNumber(hours) + _separator;
                        break;
                    case 'M':
                        result += GetNumber(minutes) + _separator;
                        break;
                    case 'S':
                        result += GetNumber(seconds) + _separator;
                        break;
                }
            }
            result = result.Substring(0, result.Length - 1);
            //return result;
            return hours + ":" + minutes + ":" + seconds;
        }

        public IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
            for (; ; )
            {
                seconds--;
                if (seconds < 0)
                {
                    seconds = 59;
                    minutes--;
                    if (minutes < 0)
                    {
                        minutes = 59;
                        hours--;
                        if (hours < 0)
                        {
                            hours = 59;
                            days--;
                            if (days < 0)
                            {
                                IsAvailible = true;
                            }
                        }
                    }
                }
                OnSetTimeRemain();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (Type type in Enum.GetValues(typeof(Type)))
        {
            var bonus = Get(type);
            bonus.Init();
            StartCoroutine(bonus.Delay());
        }
    }

    private static string DateTime_To_String(DateTime dateTime)
    {
        return dateTime.Year + " " +
            dateTime.Month + " " +
            dateTime.Day + " " +
            dateTime.Hour + " " +
            dateTime.Minute + " " +
            dateTime.Second;
    }
    private static DateTime String_To_DateTime(string _dateTime)
    {
        string[] str = _dateTime.Split(' ');
        return new DateTime(
            int.Parse(str[0]),
            int.Parse(str[1]),
            int.Parse(str[2]),
            int.Parse(str[3]),
            int.Parse(str[4]),
            int.Parse(str[5])
        );
    }
}
