A take the McDonald's Observer Saga using NServiceBus 5 from Jimmy Bogard's NSB 2014 presentation "Scaling NServiceBus": http://fast.wistia.net/embed/iframe/y56svovwnk?popover=true. Start at 28:00

To run the solution:
--------------------
- mark .ClassClient, .SagaEndpoint, .FriesEndpoint and .ShakeEndpoint for startup
- hit F5
- press any key to create an order
- thread.sleeps used to make menu endpoints slower (but still faster than most McDonalds!)
- console shows menu item "stations" eventually bus.Replying back to Saga, and saga keeps track of what order items went to what menu stations and when all stations are complete, saga is complete

TODO
----
- delete the Fries and Shake classes in the .Messages project and instead manage the list of items in an order as a List<string>
	- explain that it's the simplist thing that will work, and most likely would have to change to class based to carry meta-data about the menu item
		- for example, a shake has a flavor (vanilla or chocoloate), whereas a hamburger can have pickles and/or tomatos... 
		- these pieces of info about the menu item do not apply across all menue types...
			- BUT some, do.... like small, medium and large