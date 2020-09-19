using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Database;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace R2MD.Diagnostics
{
    public class ExceptionHandler
    {
        public static ExceptionManager ExManager;

        public static void BuildExceptionHandler()
        {
//#if DEBUG
//            ExManager = BuildDebugExceptionManagerConfig();
//#else
            DatabaseProviderFactory factory = new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection);
            DatabaseFactory.SetDatabaseProviderFactory(factory, false);
            LoggingConfiguration loggingConfiguration = BuildLoggingConfig();
            LogWriter logWriter = new LogWriter(loggingConfiguration);
            ExManager = BuildExceptionManagerConfig(logWriter);
//#endif
            // Create an ExceptionPolicy to illustrate the static HandleException method
            ExceptionPolicy.SetExceptionManager(ExManager);
        }

        //normal execution config
        private static ExceptionManager BuildExceptionManagerConfig(LogWriter logWriter)
        {
            var policies = new List<ExceptionPolicyDefinition>();

            var assistingAdministrators = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                     {
                       new LoggingExceptionHandler("Database", 9001, TraceEventType.Error,
                         "Ride2Md Application", 5, typeof(TextExceptionFormatter), logWriter),
                       new ReplaceHandler("Application error.  Please advise your administrator and provide them with this error code: {handlingInstanceID}",
                         typeof(Exception))
                     })
            };

            var exceptionShielding = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                    {
                    new WrapHandler("Application Error. Please contact your administrator.",
                        typeof(Exception))
                    })
            };

            var loggingAndReplacing = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                    {
                    new LoggingExceptionHandler("Database", 9000, TraceEventType.Error,
                        "Ride2Md Application", 5, typeof(TextExceptionFormatter), logWriter),
                    new ReplaceHandler("An application error occurred and has been logged. Please contact your administrator.",
                        typeof(Exception))
                    })
            };

            var logAndThrew = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                    {
                    new LoggingExceptionHandler("Database", 9000, TraceEventType.Error,
                        "Ride2Md Application", 5, typeof(TextExceptionFormatter), logWriter),
                    })
            };

            var logAndWrap = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.None,//don't threw the exception
                    new IExceptionHandler[]
                    {
                    new LoggingExceptionHandler("Database", 9002, TraceEventType.Error,
                        "Ride2Md Application", 5, typeof(TextExceptionFormatter), logWriter),
                    new ReplaceHandler("Application error will be ignored and processing will continue.",
                        typeof(Exception))
                    })
            };

            var replacingException = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                     {
                       new ReplaceHandler("Application Error. Please contact your administrator.",
                         typeof(Exception))
                     })
            };

            policies.Add(new ExceptionPolicyDefinition("AssistingAdministrators", assistingAdministrators));
            policies.Add(new ExceptionPolicyDefinition("ExceptionShielding", exceptionShielding));
            policies.Add(new ExceptionPolicyDefinition("LoggingAndReplacingException", loggingAndReplacing));
            policies.Add(new ExceptionPolicyDefinition("LogAndThrewException", logAndThrew));
            policies.Add(new ExceptionPolicyDefinition("LogAndWrap", logAndWrap));
            policies.Add(new ExceptionPolicyDefinition("ReplacingException", replacingException));
            return new ExceptionManager(policies);
        }
        private static LoggingConfiguration BuildLoggingConfig()
        {
            // Formatters

            TextFormatter formatter = new TextFormatter("Timestamp: {timestamp}{newline}Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}EventId: {eventid}{newline}Severity: {severity}{newline}Title: {title}{newline}Activity ID: {property(ActivityId)}{newline}Machine: {localMachine}{newline}App Domain: {localAppDomain}{newline}ProcessId: {localProcessId}{newline}Process Name: {localProcessName}{newline}Thread Name: {threadName}{newline}Win32 ThreadId:{win32ThreadId}{newline}Extended Properties: {dictionary({key} - {value}{newline})}");
            // Listeners
            var databaseTraceListener = new FormattedDatabaseTraceListener(DatabaseFactory.CreateDatabase("Conn"), "WriteLog", "AddCategory", formatter);
            // Build Configuration
            var config = new LoggingConfiguration();
            config.AddLogSource("Database", SourceLevels.All, true).AddTraceListener(databaseTraceListener);

            // Special Sources Configuration

            return config;
        }

        //debug execution config
        private static ExceptionManager BuildDebugExceptionManagerConfig()
        {
            var policies = new List<ExceptionPolicyDefinition>();

            var assistingAdministrators = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                     {
                       new ReplaceHandler("Application error.  Please advise your administrator and provide them with this error code: {handlingInstanceID}",
                         typeof(Exception))
                     })
            };

            var exceptionShielding = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                    {
                    new WrapHandler("Application Error. Please contact your administrator.",
                        typeof(Exception))
                    })
            };

            var loggingAndReplacing = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                    {
                    new ReplaceHandler("An application error occurred and has been logged. Please contact your administrator.",
                        typeof(Exception))
                    })
            };

            var logAndThrew = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]{})
            };

            var logAndWrap = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                     {
                       new WrapHandler("An application error has occurred.",
                         typeof(Exception))
                     })
            };

            var replacingException = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof (Exception),
                    PostHandlingAction.ThrowNewException,
                    new IExceptionHandler[]
                     {
                       new ReplaceHandler("Application Error. Please contact your administrator.",
                         typeof(Exception))
                     })
            };

            policies.Add(new ExceptionPolicyDefinition("AssistingAdministrators", assistingAdministrators));
            policies.Add(new ExceptionPolicyDefinition("ExceptionShielding", exceptionShielding));
            policies.Add(new ExceptionPolicyDefinition("LoggingAndReplacingException", loggingAndReplacing));
            policies.Add(new ExceptionPolicyDefinition("LogAndThrewException", logAndThrew));
            policies.Add(new ExceptionPolicyDefinition("LogAndWrap", logAndWrap));
            policies.Add(new ExceptionPolicyDefinition("ReplacingException", replacingException));
            return new ExceptionManager(policies);
        }
    }
}