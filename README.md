🚤 Boat Manager – OWT Test Project
This is a basic boat management application built for the OWT technical test.
It follows the Clean Architecture approach, heavily inspired by Jason Taylor's implementation.

🎥 Reference video: Jason Taylor - Clean Architecture
https://www.youtube.com/watch?v=dK4Yb6-LxAk

🛠️ Tech Stack

Backend
.NET 9 – API implementation
MediatR – CQRS pattern
FluentValidation – Input validation
AutoMapper – Object mapping
Ardalis.GuardClauses – Defensive programming
NSWAG – API documentation & client generation

Frontend
React 19 – Modern frontend framework
TypeScript – Strict typing & maintainability
Tailwind CSS – Styling
React Query – Data fetching & caching
Zustand – Global state management

🏗️ Architecture
The project follows Clean Architecture, separating:
✅ Domain – Core business logic
✅ Application – CQRS, validation & use cases
✅ Infrastructure – Database, authentication, external services
✅ Presentation – Web API & React frontend

🚀 This structure is a bit overkill for this test, but I had fun implementing it!
There's still room for improvement, but I'm happy to have completed the test in just 3 days.

🚀 Features Implemented
✅ User authentication (Login, Register, JWT, Refresh Token)
✅ Boat CRUD (Create, Read, Update, Delete boats)
✅ Pagination & Page Size Selection
✅ Role-based authorization
✅ NSwag-generated API client
✅ Automatic token refresh
✅ Global error handling
✅ Clean & modern UI with Tailwind CSS
