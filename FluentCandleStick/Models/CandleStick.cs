﻿namespace fluenttechFinancial.Models
{
    public class CandleStick
    {
        public DateTime Minute { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
    }
}
