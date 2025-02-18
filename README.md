# 🚤 Boat Manager – OWT Test Project  

![.NET](https://img.shields.io/badge/.NET-9-blue?style=flat&logo=dotnet)  
![React](https://img.shields.io/badge/React-19-61DAFB?style=flat&logo=react)  
![License](https://img.shields.io/badge/License-MIT-green.svg)  

This is a **boat management application** built for the **OWT technical test**.  
It follows the **Clean Architecture** approach, inspired by **Jason Taylor**.  

📺 **Reference video:** [Jason Taylor - Clean Architecture](https://www.youtube.com/watch?v=dK4Yb6-LxAk)  

---

## 🛠️ **Tech Stack**  

### **Backend**  
- ⚙️ **.NET 9** – Web API implementation  
- 📌 **MediatR** – CQRS pattern  
- ✅ **FluentValidation** – Request validation  
- 🔄 **AutoMapper** – Object mapping  
- 🚦 **Ardalis.GuardClauses** – Defensive programming  
- 📝 **NSWAG** – API documentation & client generation  

### **Frontend**  
- ⚛️ **React 19** – Modern frontend framework  
- 🔵 **TypeScript** – Strongly typed JavaScript  
- 🎨 **Tailwind CSS** – Utility-first styling  
- 🔍 **React Query** – Data fetching & caching  
- 🌎 **Zustand** – State management  

---

## 🏢 **Architecture Overview**  

The project follows **Clean Architecture**, separating:  

📂 **Domain** – Core business logic  
📂 **Application** – CQRS, validation & use cases  
📂 **Infrastructure** – Database, authentication, external services  
📂 **Presentation** – Web API & React frontend  

⚡ **This architecture might be overkill for the task, but I had fun implementing it!**  
I finished the test in **3 days**, but there's still room for improvement.  

---

## 🚀 **Features Implemented**  

✔️ **User authentication** (Login, Register, JWT, Refresh Token)  
✔️ **Boat CRUD** (Create, Read, Update, Delete boats)  
✔️ **Pagination & Page Size Selection**  
✔️ **Role-based authorization**  
✔️ **NSwag-generated API client**  
✔️ **Automatic token refresh**  
✔️ **Global error handling**  
✔️ **Clean & modern UI with Tailwind CSS**  
✔️ **Unit Testing**  

---

## 📌 **How to Run the Project**  

### **Backend (.NET 9)**
```sh
cd backend
dotnet run
```

### **Frontend (React 19)**
```sh
cd frontend
npm install
npm run dev
```

---

## 🛠️ **Future Improvements**  
💡 Improve **UI/UX animations**  
💡 Implement **Docker & CI/CD pipeline**  
💡 Add **search & filtering for boats**  
