# Software Engineering Demo

## Introduction

* This solution is made up of 3 projects.
    * WebAPI - A simple Restful API that takes and stores comments identified by an SKU.
            The API consists of of a single endpoint with Get, Post and Put actions. The Get and Post actions
            are documented by Swagger are are intended for general consumption. The Put action is intended for 
            internal use and could be protected by 'scopes' to limit access (Not implemented).

    * WebAPITests - Tests covering the WebApi functionality.

    * ToneAnalyzerFunction - A V3 Azure Function that receives recently added comments from the Post action of the WebApi 
            as messages on a message queue. A call is then made to the IBM analyzer API (using the providied sdk) to obtain
            a 'Tone' for the comment. A call is then made using an HttpClient to the Put action of the WebApi to update the 
            comment with the receive 'Tone'.

## Scaling 

In order to help the application scale it has been architected so that the API functionality is deployed separately to the 
'ToneAnalyzerFunction'. The API can be deployed to a virtual machine or inside a container based system. 
The 'ToneAnalyzerFunction' is deployed to the Azure Cloud. This allows the resources required by each to be independently 
scaled as needed.

### RESTful API

The API is restful because it makes use of a single URL for all the 'Comment' actions. It uses Get to retrieve a resource, 
Post to create a resource and Put to change/update a resource. It also returns appropriate response codes for the performed action. 
The API also supports ‘application/json’ which is one of the formats of REST and also conforms to a Level 2 REST API.

### Code structure

The approach taken is to break down the required functionality into separate applications. (The Web API and the Tone Analyzer) and where 
required break the application into small classes or components. The underlying objective is to have small highly cohesive clases with
a single responsibility. Program to interfaces rather than concretions by making use of DI. Avoiding the use of magic strings.

### API documentation

The Get and Post actions are documented by Swagger. The 'Put' action has been intentionally omitted. The reason being that the Put action 
can be included in the Swagger documentation when some security has been enabled around it. 


### What's mising

* This is not a complete application and some functionality has ben omitted.
    * The functionality to generate the Azure servicebus message. The intention was that the service processing the Post request would 
        also orchestrate the generation of the service bus message. This has not been implemented.

    * A Test project to test the functionality of the ToneAnalyzerFunction project.

    * Logging, Use of logging component such as Serilog / Application Insights and more logging throughtout a request journey.


### Some asumptions

* The 'Response' property returned on the call to the IBM Tone API contains the tone.
* Only a single consumer required to process the service bus message.

