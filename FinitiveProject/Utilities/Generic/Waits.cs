
using System;

namespace SeleniumAutomation.Utilities
{
	public class Waits 
	{
		private static int SHORTWAIT;
		private static int NORMALWAIT;
		private static int LONGWAIT;
		private static int VERYLONGWAIT;
		private static int IMPLICITWAIT;
        
        public void setWaits()
		{
            SHORTWAIT = new AutomationUtilities().SelectTimeCount("SHORTWAIT");
            NORMALWAIT = new AutomationUtilities().SelectTimeCount("NORMALWAIT");
            LONGWAIT = new AutomationUtilities().SelectTimeCount("LONGWAIT");
            VERYLONGWAIT = new AutomationUtilities().SelectTimeCount("VERYLONGWAIT");
            IMPLICITWAIT = new AutomationUtilities().SelectTimeCount("IMPLICITWAIT");
		}
		
		public static int getsShortWait()
		{
			return SHORTWAIT;
		}
		
		public static int getsNormalWait()
		{
			return NORMALWAIT;
		}
		
		public static int getsLongWait()
		{
			return LONGWAIT;
		}
		
		public static int getsVeryLongWait()
		{
			return VERYLONGWAIT;
		}
		
		public static int getsImplicitWait()
		{
			return IMPLICITWAIT;
		}
	}
}
