Feature: Login
  As a user
  I want to be able to login to the application
  So that I can access my account

Scenario: Successful Login
  Given User is on the login page
  When User enters valid username and password
  And User clicks on the login button
  Then User should be logged in successfully