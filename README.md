# 💖 TrueMatch - A Modern Dating Website Platform

TrueMatch is a fully responsive, user-friendly dating website developed using **ASP.NET Core MVC**. It enables users to connect, manage profiles, send friend requests, and chat with matches in a secure and interactive environment.

![TrueMatch Banner](assets/images/banner.jpg) <!-- Replace with an actual image path or GitHub-hosted image -->

---

## 🌐 Live Demo

🚀 _Coming soon_ or host it via [Azure](https://portal.azure.com) / [Render](https://render.com) / [Netlify](https://www.netlify.com/) (for static frontend only)

---

## 📌 Features

- 🔐 **User Authentication** (Sign Up, Log In, Log Out)
- 👤 **User Profiles** with editable settings
- ❤️ **Friend Requests & Friend List**
- 💬 **Private Messaging (Chat System)**
- 📚 **Success Stories & Blog**
- 📞 **Contact Us** and newsletter support
- 🎨 Responsive and animated **UI/UX** with Bootstrap & jQuery
- 🌐 Session management using `HttpContext.Session`

---

## 🛠️ Technologies Used

| Layer | Tools |
|-------|-------|
| **Frontend** | HTML5, CSS3, Bootstrap 5.3, jQuery,js |
| **Backend** | ASP.NET Core MVC |
| **Authentication** | ASP.NET Identity, Session |
| **Database** | SQL Server / SQLite (choose during setup) |
| **IDE** | Visual Studio 2022 |
---

## 📁 Project Structure

```plaintext
TrueMatch/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── UserAuthController.cs
│   ├── UserProfileController.cs
│   ├── ChatController.cs
│   └── ...
│
├── Models/
│   ├── User.cs
│   ├── Message.cs
│   └── ...
│
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml
│   ├── Home/
│   ├── UserAuth/
│   ├── UserProfile/
│   └── ...
│
├── wwwroot/
│   ├── assets/
│   │   ├── css/
│   │   ├── js/
│   │   └── images/
│
├── appsettings.json
└── TrueMatch.csproj

## Screenshots

### User Interface

#### Login/Signup
![Login Page](/ScreenShot/login.png)
![Signup Page](ScreenShot/signup.png)

#### Profile Page
![Profile Page](/ScreenShot/Profile.png)
![Update Profile Page](/ScreenShot/UpdateProfile.png)
![Updated Profile Page](/ScreenShot/UpdatedProfile.png)

#### User Post
![Post Page](/ScreenShot/Post.png)
![User Post](/ScreenShot/UserPost.png)

#### Search User
![Search Home](/ScreenShot/SearchHome.png)
![Search Result](/ScreenShot/SearchResult.png)

#### Friend
![Friend](/ScreenShot/friend.png)
![Friend Search Result](/ScreenShot/SearchResult.png)

#### Message
![Message](/ScreenShot/message.png)
![Message Chat](/ScreenShot/chat.png)

### Admin Interface

#### Login
![Admin Login](/ScreenShot/adminLogin.png)

#### Admin Panel
![Admin Panel](/ScreenShot/AdminPanel.png)
![User Complain](/ScreenShot/UserComplain.png)
![User List](/ScreenShot/userlist.png)



