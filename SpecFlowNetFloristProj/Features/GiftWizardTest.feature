Feature: Testing Gift Wizard Section
    As a user of NetFlorist website
    I want to be able to find and add available products to my basket

    Scenario: Testing product availability
        Given I am on the gift wizard section of the website
        When I select the occasion, location, and date
        And I click on the Find Now button
        Then I should see a list of all available products
        When I test each product's availability
        Then I should be able to add all available products to my basket
        And I should see an error message for any unavailable products
        When I check for the availability of products on the next page (if available)
        Then I should continue the testing process until all available products have been tested