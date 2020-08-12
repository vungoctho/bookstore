Background: a bookstore wants to grow their market size and decides to use e-commerce. They want to build a website which allow their customer to be able browse a variety of books, and buy it online.

Scope: with purpose of demonstration, I just implement some basic functions from client to server such as:
 - Allow user to browse a variety of books via Search
 - Provide Buy a specified book
 - Receive buy request from user, and produce it to MessageQueue in Server
 - Use Redis MessageQueue as Message Bus which allow many services to communicate together

Implementation: There are 3 main projects
 - BookStoreUI: use Vue3 + Vite with a single page provides user the searching and buying book
 - BookStore Web Api: provides Api to search and buy book. The api request will be authenticated with API Key which attached in Header
   Noted: the API Key is hard coded with value "123" just for demonstration
  + the request will be transformed to message queue and send to bus
 - BookStore.MessageHandlerService: a background service subcribes the purchase book message from bus.
	From there, we can do several logics such as database persistency, or even send next message to bus ( e.g. Payment, Statistic ... )

Design core:
 - Microservice and Message Queue ( e.g. Redis Message Queue)
 - Follow SOLID principals to ensure coding is able to change easily, e.g. use other MQ like RabbitMQ
 - With MQ, we can grow the system easily by adding more services subcribe a specified message. Avoid touching the existing service
 - UI: use Vue3 as the newest version and Vite in order to take lot of advance benefits, such as build speed of Vite, Vue3 Composition API syntax ...
 

How to run:
 1. Start Redis Server by run redis-server.exe in Redis-x64-3.2.100.zip. Let it run on the default port "localhost:6379"
 2. Open BookStore.sln in VS2019
	2.1 Run BookStore.Web.Api with this specified port http://localhost:5000/ => this port is configured in client already
	2.2 Run BookStore.MessageHandlerService background service which will receive PurchaseBook request message
 3. Open BookStoreUI in Visual Code, and run command
	- npm install 
	- npm run dev
	=> Noted: for demonstration I hard coded the web api address to localhost:5000 when calling get/buy book api
	
Happy coding!
	
 