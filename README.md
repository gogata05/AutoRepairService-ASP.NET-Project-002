AutoRepairService-ASP.NET-Project-002

# AutoRepairService

## Introduction
AutoRepairService is application for creating/accepting repairs requests and for buying AutoRepairService car.

## Features
- Google Maps with Econt offices in the cart view
- Live Support chat added for support user/admin

### User Roles
- **Customer**: Upon registration, can Add, Edit, Access All repairs, View My repairs, Accept/Decline offers, Search, Use Live support Chat, View their orders, Buy car, Rate the mechanic with stars
- **Mechanic**: customer can become mechanic from mechanics/Join Our mechanics button, can send offers for repairs
- **Admin**: Seeded, access Admin page, Add/Edit repairs, Add/Edit/Delete cars, Accept/Decline repairs, Complete Orders, View repairs statistics, Reply to Message Requests Live Support Chat

## Role Details:

### customer Role
- Customers can Add repairs  
- The repair is sent for admin review and can be accepted or declined.  
- Customer can Edit repair before it is Accepted from the Admin  
- If the repair is Taken or Accepted it can not be deleted  
- Receive offers  
- Customer can receive offer for his repair(repairs/My repair Offers - one offer from mechanic)  
- Offers can be accepted or declined:  
- If offer is accepted the current repair is marked as "Taken"
- When the repair is completed customer can mark the repair as "Completed" from repairs/My repairs and rate the mechanic.  
- Search cars  
- cars can be added to user's cart where quantity is selected, address is required to submit the order. From the menu button the user can review his order status.  
- Rate the mechanic with stars after repair completed

### Mechanic Role
- Customer can become mechanic from mechanics/Join Our mechanics button  
- Mechanics can send offers for all available repairs in the repairs menu  
- Mechanic can Search cars  
- cars can be added to user's cart where quantity is selected, address is required to submit the order. From the menu button the user can review his order status.

### Admin Role
- Can access Administration page:  
- Add/Edit repairs in All repairs
- Accept/Decline repairs in Admin area 
- Add/Edit/Delete cars
- Review Orders and mark them as Completed  
- View repairs statistics
- Reply to Message Requests

## Roles logins:
- Customer: `username: customer`, `password: customer`
- Mechanic: `username: mechanic`, `password: mechanic`
- Admin: `username: admin`, `password: admin`

## How to use?
- 0.Download the repository and extract it to folder
- 1.Open AutoRepairService.sln with visual studio 2022
- 2.In appsetting.json add your personal "ConnectionStrings"
- 3.right click on AutoRepairService Project and "Set as Startup Project"
- 4.In "Package Manager Console" with Default project: "AutoRepairService.Infastructure" type: update database
- 5.Ctrl+F5
- 6.Open Url localhost on your browser: https://localhost:7160/
- 7.Enjoy!


## Used libraries:
    - `SignalR` - for realtime live chat
    - `Pace`, - for page load progress bars
    - `Toastr` - for notifications 
    - `jQuery` - simplifying html and css
    - `bootstrap` - CSS Framework

## Database

SSMS and MS SQL are used for storing & managing the data.

## Tests

- `Unit Tests`

## Demo
Live demo at Replit -

## Photos

Home page with the last 3 cars added:

![image](Images/Home%20page%20with%20the%20last%203%20cars%20added.png) 

Live Chat Support between customer and admin:


![image](Images/Live%20Chat%20Support%20between%20customer%20and%20admin.png)


Cart with Google Maps and Econt offices:

![image]()


Car shop:

![image](Images/all%20cars%20for%20sale%20can%20u%20can%20put%20in%20the%20cart.png) 



Customer can Rate with 1/5 stars The mechanic:

![image](Images/customer%20can%20rate%20with%2015%20start%20the%20mechanic.png) 



Add repair request:

![image](Images/Add%20a%20car%20for%20repair.png) 



My Created repairs:

![image](Images/My%20Repair%20Requiests.png) 



Accept or Decline offers:

![image](Images/Admin%20can%20approve%20or%20decline%20repairs.png) 



My Orders:

![image](Images/my%20orders.png) 



Register:

![image](Images/register.png) 



Mechanic can send offers:

![image](Images/mechanic%20can%20send%20repair%20offers.png) 



Admin can add car:

![image](Images/admin%20can%20add%20cars%20for%20sale.png) 



Admin can edit/delete car:

![image](Images/Admin%20can%20edit%20and%20delete%20cars.png) 



Admin can View All Orders and Mark as Complete any Order:

![image](Images/admin%20can%20mark%20the%20orders%20as%20completed.png) 





Admin can View repair Statistics:

![image](Images/app%20total%20Statistics%20repair.png) 
