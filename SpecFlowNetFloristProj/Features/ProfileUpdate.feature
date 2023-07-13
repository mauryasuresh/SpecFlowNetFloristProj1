Feature: ProfileUpdate

As a user
  I want to be able to update my profile information
  So that my profile is up-to-date
@tag1
Scenario: Successful Profile Update
	Given User is logged in
  And User is on the profile update page
  When User enters valid profile information
  And User clicks on the update button
  Then User should see a success message
