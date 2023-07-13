Feature: BackToLogin and Registration Feature

A short summary of the feature

@BackToLogin
Scenario: Verify Back To Login and Register feature 
	Given User is on home page
	When User add product to basket	
	And  Redirect to recepient details page and click on register new account 
	Then  User Should see "Back to login" button 
	And User Should see "Register" button


