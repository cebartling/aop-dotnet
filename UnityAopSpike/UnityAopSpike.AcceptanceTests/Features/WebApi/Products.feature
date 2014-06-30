Feature: Products ReST API

Scenario: Retrieve a representation of products 
	Given products exist in the system
	When I attempt to retrieve the products through the appropriate ReST API
	Then the request should be successful
	And a representation of products should be returned in JSON format
