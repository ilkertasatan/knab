You can find my answer in the below;

### Questions:

1. How long did you spend on the coding assignment? What would you add to your solution if you had
   more time? If you didn't spend much time on the coding assignment then use this as an opportunity to
   explain what you would add.
2. What was the most useful feature that was added to the latest version of your language of choice?
   Please include a snippet of code that shows how you've used it.
3. How would you track down a performance issue in production? Have you ever had to do this?
4. What was the latest technical book you have read or tech conference you have been to? What did you
   learn?
5. What do you think about this technical assessment?
6. Please, describe yourself using JSON


### Answers

1. I spent approximately 2 days to complete the assignment. If I had more time; I would add an identity server to make the api more secure and add e2e tests and also integrate cloud services on Azure such as KeyVault to store my sensitive settings.
2. I really like pattern matching. Here is code snippet;

````c#
public static IActionResult For(ICommandResult output) =>
   output switch
   {
       LoginUserCommandResult result => Ok(result),
       UserNotFoundResult result => NotFound(result),
       PasswordInCorrectResult => Unauthorized(),
       _ => DefaultResult()
   };
````

3. I would use APIM tools in order to monitor what's happening in the services. In the past I've used DynaTrace to monitor performance issues.
4. I've read several articles about .NET 6. I usually follow the articles on the internet.
5. I would think about my assignment having good architecture and clean and readable and maintainable code. But, I never say that my application is perfect because every application may have glitches.
6. Here is the JSON;

````json
{
	"person": {
		"name": "Ilker",
		"surname": "Tasatan",
		"age": 36,
		"born_in": "Istanbul / Turkey",
		"lives_in": "Diemen / The Netherlands",
		"is_married": true,
		"hobbies": [
			"aviation",
			"travel",
			"watching movies",
			"cycling"
		],
		"job": {
			"title": "Senior Software Engineer",
			"current_company": "Effectory",
			"total_experince": "17 years"
		},
		"career": {
			"next_step": "Being principal engineer"
		},
		"skills": [
			"C#",
			".NET 6",
			"Web Api",
			"Microservices",
			"Event Driven Architecture",
			"Event Sourcing",
			"CQRS",
			"DDD",
			"TDD",
			"BDD",
			"Relational DB",
			"Non-relational DB",
			"Message Brokers",
			"Azure",
			"Azure Devops"
		]
	}
}
````
