# powerplant-coding-challenge

## Hello !

My name is Monica Stanea

## explaining how to build and launch the API:

in Visual Studio 2022:
Build the Api:
	- under the menu "Build", the option "Build solution"
	or
	- in the "Solution explorer" window, right click on the solution, click the option "Build solution"

Launch the Api:
	- Ctrl + F5
	or
	- under the menu "Debug", the option "Start Without Debugging"
	That will launch the Swagger with the Api on "http://localhost:8888/swagger/index.html"
	=> Try it out with the correct data for the body => Execute
	or
	in Postman, a get request on the url "http://localhost:8888/productionplan", with the entry data Json in body



## The entry data for the POST request
The examples of entry data is in Swagger, under "Request body":
Is a Json containing 3 types of data:

1. load: integer
2. fuels: with a list of the informations about the fuels/costs of each powerplant:   
	- gas : cost of the gas to produce an unity of MWh, in euro/MWh
	- kerosine : cost of the kerosine to produce an unity of MWh, in euro/MWh
	- co2 : the cost of Co2 emission in euro/ton
	- wind : average percentage of wind %; Wind-turbines do not consume 'fuel' and thus are considered to generate power at zero price 
3. powerplants: the list of powerplants at disposal to generate the demand load, with following informations:
	- name: the name of the powerplant
	- type: gasfired, turbojet or windturbine
	- efficiency: the efficiency at which they convert a MWh of fuel into a MWh of electrical energy
	- pmax: the maximum amount of power the powerplant can generate
	- pmin: the minimum amount of power the powerplant generates when switched on

## The response

Is a Json containing a list of powerplants with the energy that should each deliver that the total sum will be equal to the "load"