using System.Collections.Generic;
using System.Linq;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;

namespace Insight.Web.BddTests.PageObjects.Pages
{
    public class WidgetPage : BasePage
    {
        private static List<HoldingStats> NephilaStatisticsTable;

        public static bool ChangeWindowAndVeryifyAnalysisPage(string analysisPage)
        {
            ChangeBrowserInstanceToNewWindow("QATest Dashboard-Risk Report Holding Statistics | Insight v1.0.0.0");
            WaitForAjax(60);
            Driver.Instance.SwitchTo()
                .Frame(
                    Driver.Instance.FindElement(By.CssSelector(".section-content .widget:first-of-type .chartContainer")));

            return Driver.Instance.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[7]")).Text == analysisPage;
        }

        public static List<IWebElement> ChangeIframeAndreturnDataFromWidget(string iframe)
        {
            Driver.Instance.SwitchTo().DefaultContent();
            Driver.Instance.SwitchTo()
                .Frame(
                    Driver.Instance.FindElement(By.CssSelector(iframe)));

            return Driver.Instance.FindElements(By.CssSelector(".nephilaStatisticsTable tr td")).ToList();
        }

        public static List<HoldingStats> GenerateNephilaStatsTable(List<IWebElement> statsTable)
        {
            var counter = 0;
            var holdingstatsCount = 1;
            var hs = new HoldingStats();
            ;

            var nst = new List<HoldingStats>();
            foreach (var st in statsTable)
            {
                if (counter > 0 && counter != 10)
                {
                    // Name
                    switch (holdingstatsCount)
                    {
                        case 1:
                            hs.Name = st.Text.Trim();
                            break;
                        case 2:
                            hs.Currency = st.Text.Trim();
                            break;
                        case 3:
                            hs.Percentage = st.Text.Trim();
                            break;
                    }


                    if (holdingstatsCount == 3)
                    {
                        nst.Add(hs);
                        holdingstatsCount = 1;
                        hs = new HoldingStats();
                    }
                    else
                    {
                        holdingstatsCount++;
                    }
                }

                counter++;
            }
            return NephilaStatisticsTable = nst;
        }
    }
}