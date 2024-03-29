﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

public static class ExtentManager
{

    private static ExtentReports extent;
    private static ExtentHtmlReporter htmlReporter;
    
    public static ExtentReports GetExtent()
    {
        if (extent == null)
        {
            string reportPath = @"C:\Users\Suresh Maurya\source\repos\SpecFlowNetFloristProj1\SpecFlowNetFloristProj\ExtentReports\extentReport.html";
            htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        return extent;
    }
}

