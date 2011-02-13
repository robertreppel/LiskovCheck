Feature: Semi-automatic check for adherence to the Liskov Substitution Principle.
	
	Definition of the Liskov Substitution Principle:

	"Liskov substitution principle (LSP) is a particular definition of a 
	subtyping relation, called (strong) behavioral subtyping,"

		(from http://en.wikipedia.org/wiki/Liskov_substitution_principle)

	...also known as:

	"If it looks like a duck, quacks like a duck, but needs batteries – you probably have the wrong abstraction."

		(from http://www.lostechies.com/blogs/derickbailey/archive/2009/02/11/solid-development-principles-in-motivational-pictures.aspx )

	For more information, see http://birding.about.com/od/birdprofiles/tp/typesofducks.htm

@mytag
Scenario: A subtype is more likely to adhere to the Liskov Substitution Principle
	Given a DLL named Zoo.dll with a "Duck" class which inherits from "Animal"
	When I run "liskovcheck Zoo.dll"
	Then the words "It looks like a Duck and behaves like an Animal" should be on the screen.

Scenario: A subtype is less likely to adhere to the Liskov Substitution Principle
	Given a DLL named Zoo.dll with a "MerganserDuck" class which inherits from "TransistorRadio"
	When I run liskovcheck.exe with the argument 'Zoo.dll'"
	Then the words "It looks like a MerganserDuck and behaves like a TransistorRadio" should be on the screen.

Scenario: No file exists at the assembly DLL location specified in the command line.
	Given a DLL named SomeAssembly.dll which doesn't exist
	When I run "liskovcheck SomeAssembly.dll"
	Then the message "No file found at 'SomeAssembly.dll'"

Scenario: Help information is displayed.
	Given no command line arguments
	When I run "liskovcheck"
	Then I should see "Usage example: 'liskovcheck 'c:\path\to\assembly\MyAssembly.dll'"
	And  "Semi-automatic check for adherence to the Liskov Substitution Principle"
	

