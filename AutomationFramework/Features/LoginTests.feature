Feature: Login
	
Scenario: Error message displayed for invalid credentials
	Given the user navigates to login page
	When the user types in the following username: 'invalid user' 
	And the user types in the following password: 'invalid password'
	And the user clicks on the login button 
	Then invalid user details error message is displayed: 'Invalid username or password'
