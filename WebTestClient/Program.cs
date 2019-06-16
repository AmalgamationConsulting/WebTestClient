namespace WebTestClient
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary><c>Program</c> contains the entry point for the Web Test Client.</summary>
    internal static class Program
    {
        /// <summary>The entry point for the Web Tests Client.</summary>
        [method: STAThread]
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        internal static void Main()
        {
            ServiceSection section = (ServiceSection)ConfigurationManager.GetSection("services");
            ApplicationElementCollection applications = section.Applications;
            while (true)
            {
                try
                {
                    int alignment = (int)Math.Ceiling(Math.Log10(applications.Count + 1));
                    string format = "{0," + alignment + "}";

                    Console.Clear();
                    Console.WriteLine("Main Menu");
                    Console.WriteLine();
                    for (int i = 0; i < applications.Count; i++)
                    {
                        Console.Write(string.Format(NumberFormatInfo.InvariantInfo, format, i + 1));
                        Console.Write(". ");
                        Console.WriteLine(applications[i].Name);
                    }

                    Console.WriteLine();
                    int? option = ConsoleUtility.PromptForInteger("Please select an option: ", 1, applications.Count);
                    Console.WriteLine();

                    if (option.HasValue)
                    {
                        ApplicationMenu(applications[option.Value - 1]);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleUtility.ReportException(ex);
                    ConsoleUtility.PauseAndWait();
                }
            }

            Console.Clear();
        }

        /// <summary>Displays the application menu.</summary>
        /// <param name="application">The <see cref="ApplicationElement"/> for the selected web service application.</param>
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        private static void ApplicationMenu(ApplicationElement application)
        {
            ServiceElementCollection services = application.Services;
            while (true)
            {
                int alignment = (int)Math.Ceiling(Math.Log10(services.Count + 1));
                string format = "{0," + alignment + "}";

                Console.Clear();
                Console.WriteLine("Application Menu: " + application.Name);
                Console.WriteLine();
                for (int i = 0; i < services.Count; i++)
                {
                    Console.Write(string.Format(NumberFormatInfo.InvariantInfo, format, i + 1));
                    Console.Write(". ");
                    Console.WriteLine(services[i].Name);
                }

                Console.WriteLine();
                int? option = ConsoleUtility.PromptForInteger("Select an option: ", 1, services.Count);
                Console.WriteLine();

                if (option.HasValue)
                {
                    InvokeService(services[option.Value - 1]);
                }
                else
                {
                    break;
                }

                ConsoleUtility.PauseAndWait();
            }
        }

        /// <summary>Invokes a web service.</summary>
        /// <param name="element">The <see cref="ServiceElement"/> for the web service configuration.</param>
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        private static void InvokeService(ServiceElement element)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(element.Address);
            request.Method = element.Method;
            request.Credentials = CredentialCache.DefaultCredentials;

            if (element.Method == "POST")
            {
                byte[] entityBody = Encoding.UTF8.GetBytes(element.EntityBody);
                request.ContentType = element.ContentType;
                request.ContentLength = entityBody.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(entityBody, 0, entityBody.Length);
                    requestStream.Flush();
                }
            }

            HttpWebResponse response = null;
            try
            {
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        response = (HttpWebResponse)ex.Response;
                    }
                }

                byte[] buffer;
                using (Stream responseStream = response.GetResponseStream())
                {
                    buffer = ReadBytes(responseStream, (int)response.ContentLength);
                }

                Console.WriteLine();
                Console.WriteLine("Status Code: " + (int)response.StatusCode);
                Console.WriteLine("Status Description: " + response.StatusDescription);
                Console.WriteLine("Response:");
                Console.WriteLine(Encoding.UTF8.GetString(buffer));
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        /// <summary>Reads exactly <paramref name="count"/> bytes from <paramref name="stream"/>.</summary>
        /// <param name="stream">The stream from which bytes are read.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The array of bytes read from <paramref name="stream"/>.</returns>
        private static byte[] ReadBytes(Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            int offset = 0;

            while (offset < count)
            {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                {
                    throw new EndOfStreamException();
                }

                offset += read;
            }

            if (offset != count)
            {
                throw new InvalidOperationException();
            }

            return buffer;
        }
    }
}
