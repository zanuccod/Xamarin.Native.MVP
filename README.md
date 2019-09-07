# Xamarin.Native.MVP

In this example we explain a simple way to use **Model View Presenter (MVP)** pattern to develop a mobile application with Xamarin platform.

A MVP pattern, derived from Model View Controller (MVC), use a Presenter as a middle layer between the View and the Model.
The Presenter retrive data from the businner logic and returns it to the View but, unlike the typical MVC, it also decides what happends when you interact with the View. 

The MVP provides modularity, testability and, in general, a more clean and maintenable code.

The application is composed of a simple list of item, in this case we decide a list of students, and a separate view to create new item and to show properties of items previously created at the main list.

The example is composed of two projects: ***Common*** and ***MVP.Example***, and both are correlated with the related test project.

The decision to develop all common function on separate project is for multi platform purpouses. 
If we want to implement same application on IOS we have only to implement the views and to implements view functions with the function offers by the presenter defined in the common project.
