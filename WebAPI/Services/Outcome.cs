namespace WebAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static WebAPI.Constants.General;

    public class Outcome
    {
        public bool Successful { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public int Reason { get; set; }

        public override string ToString()
        {
            if (Successful)
            {
                return OutcomeSuccess;
            }

            return OutcomeFailed + String.Join("; ", Errors.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public static Outcome<TResult> Fail<TResult>(params string[] errors)
        {
            return new Outcome<TResult>
            {
                Successful = false,
                Errors = errors.ToList()
            };
        }

        public static Outcome<TResult> Fail<TResult>(int reason, params string[] errors)
        {
            Outcome<TResult> outcome = Fail<TResult>(errors);
            outcome.Reason = reason;
            return outcome;
        }

        public static Outcome<TResult> Success<TResult>(TResult result)
        {
            return new Outcome<TResult>
            {
                Successful = true,
                Result = result
            };
        }
    }

    public class Outcome<TResult> : Outcome
    {
        public TResult Result { get; set; }
    }
}
