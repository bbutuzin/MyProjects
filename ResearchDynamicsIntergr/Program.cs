

using System.Net.Http.Headers;
using System.Text;

class Program {


    static async Task Main(string[] args) {

      
       //Saving OATH token data in variables that was given in the task
        var clientId = "f5104fd2-6af1-4a52-8e18-97ac5102c9fc";
        var clientSecret = "4Mq8Q~SYLjYYV6P1WulHy.SKtATqULXZWx8MHccT";
        var resource = "00000015-0000-0000-c000-000000000000";
        var tokenEndpoint = "https://login.windows.net/be-terna.com/oauth2/token";

       //Creating an HTTP client instance because SOAP protocol can be carried through it
        using (var httpClient = new HttpClient()) {
       //Data is saved in key value pairs - we are taking the data from the Oath token data we saved before
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("resource", resource),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            });
        //Through POST request using the endpoint and all of the values  saved in content we get the Oath token
            var response = await httpClient.PostAsync(tokenEndpoint, content);
        //Checking if the response is successfull and if not writing the error status code so we can see the problem and solve it
            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Token Response:");
                Console.WriteLine(responseContent);
            } else {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        //Saving the access token data in a string variable for later use
        string oauthToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiIwMDAwMDAxNS0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mZjc2ZDU4MS0zNTY5LTRmNTAtOWZlNi04MWQ3ZjMxNjYzNDUvIiwiaWF0IjoxNjk0MDA0NjIyLCJuYmYiOjE2OTQwMDQ2MjIsImV4cCI6MTY5NDAwODUyMiwiYWlvIjoiRTJGZ1lHaThYdmc4S09TS3NyVElKZUg4cmhjeUFBPT0iLCJhcHBpZCI6ImY1MTA0ZmQyLTZhZjEtNGE1Mi04ZTE4LTk3YWM1MTAyYzlmYyIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2ZmNzZkNTgxLTM1NjktNGY1MC05ZmU2LTgxZDdmMzE2NjM0NS8iLCJvaWQiOiI1ZWNiNmE3Zi0yOGU5LTRjYTAtYjg3Yi0xNjQwNTY0Yzg0MDciLCJyaCI6IjAuQVJBQWdkVjJfMmsxVUUtZjVvSFg4eFpqUlJVQUFBQUFBQUFBd0FBQUFBQUFBQUFRQUFBLiIsInN1YiI6IjVlY2I2YTdmLTI4ZTktNGNhMC1iODdiLTE2NDA1NjRjODQwNyIsInRpZCI6ImZmNzZkNTgxLTM1NjktNGY1MC05ZmU2LTgxZDdmMzE2NjM0NSIsInV0aSI6InVGT09Nc0VObmsyVTVIWG5oU2ZLQUEiLCJ2ZXIiOiIxLjAifQ.gaFt2S9gLgcF8z8wv2HAS3SR3SLRO0c5YAtxa0C58FvnnKUTebZVxStPS4u3zvdLjwv58OCboe4YMuKgdOUYt88W4Iw0VsmKLnr - nVmgaITIBzwyWwRURBksBt9taRd7RPu7wkh - FpZTa3HjKgrbS6mmHByQVQjwBcHrO - RSrW5THGtmo3t813Ordhg2rH7z20PQm0Y1LMZSQHeYNnor2qA86OU - CnptO9nFJoJ7yM4841am - 4d - cz4A3_Pm3b7iTtOHL87MofPN8gRNhV6M85gD3AjO2NTOc5dPg82mdOJyIiIxKsvAnnb83 - lqgShVaStX9qcHQKCv4f3XYxvSKA";

        //Setting the api endpoint Url
        string apiUrl = "https://{{beret2.sandbox.operations.dynamics.com/soap/services/TSTimesheetServices?singleWsdl}}/api/services/TSTimesheetServices/TSTimesheetSubmissionService/createOrUpdateTimesheetLine";

        // Creating an HttpClient instance
        using (HttpClient client = new HttpClient()) {
        // Set the OAuth token in the Authorization header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

        // Define the SOAP request body
            string soapRequest = @"
                <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ser='http://example.com/'>
                    <soapenv:Header/>
                    <soapenv:Body>
                        <ser:createOrUpdateTimesheetLine>
                            <![CDATA[{
                                ""_tsTimesheetEntryList"": {
                                    ""entryList"": [
                                        {
                                            ""parmResource"": 5637145078,
                                            ""parmWorkerName"": William Flash,
                                            ""parmProjectDataAreaId"": ""ussi"",
                                            ""parmProjId"": ""00000101"",
                                            ""parmProjActivityNumber"": ""W00002480"",
                                            ""parmEntryDate"": ""2019-09-05T12:00:00"",
                                            ""parmHrsPerDay"": 4,
                                            ""customFields"": []
                                        }
                                    ]
                                }
                            }]]>
                        </ser:createOrUpdateTimesheetLine>
                    </soapenv:Body>
                </soapenv:Envelope>";

        // Create a StringContent with SOAP data
            var content = new StringContent(soapRequest, Encoding.UTF8, "application/x-www-form-urlencoded");

            try {
        // Send a POST request to the SOAP API
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

        // Check if the request was successful
                if (response.IsSuccessStatusCode) {
        // Read and process the SOAP response here
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("SOAP Response: " + responseContent);
                } else {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            } catch (Exception ex) {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
