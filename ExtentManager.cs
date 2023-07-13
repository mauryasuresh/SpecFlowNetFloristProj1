using System;

public static class ExtentManager
{

    private static ExtentReports extent;
    private static ExtentHtmlReporter htmlReporter;
    
    public static ExtentReports GetExtent()
    {
        if (extent == null)
        {
            string reportPath = @"D:\Csharp\SpecFlowNetFloristProj\SpecFlowNetFloristProj\ExtentReports\extentReport.html";
            htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        return extent;
    }
}

}