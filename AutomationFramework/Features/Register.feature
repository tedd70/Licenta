Feature: Register


Scenario: 01 User redirected to login page after registration
	Given the user navigates to login page
	And the user clicks on Register here link
	When the user fills in the user details form
	| username         | first name        | last name        | password         |
	| username<UNIQUE> | firstName<UNIQUE> | lastName<UNIQUE> | password<UNIQUE> |
	And the user clicks on Submit button
	Then the user is redirected to login page

Scenario: 02 Newly registered user can succesfully login into the application
	Given the user types in the following username: 'username<UNIQUE>' 
	And the user types in the following password: 'password<UNIQUE>'
	When the user clicks on the login button 
	Then the user is succesfully logged in into the application