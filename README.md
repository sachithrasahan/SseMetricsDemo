# Server-Sent Events (SSE) – Live System Metrics Demo

This project demonstrates how to implement **Server-Sent Events (SSE)** in an **ASP.NET Core Web API** with a **simple JavaScript + Bootstrap frontend**.

The application simulates **real-time system metrics** (CPU usage, memory usage, active users, and request rate) and streams updates from the server to the browser using SSE.

---

## 🚀 Features

- ASP.NET Core Web API with SSE endpoint
- Real-time streaming using `text/event-stream`
- Fake data generation using **Bogus**
- Automatic client reconnect support
- Event replay using `Last-Event-ID`
- Simple, responsive UI built with **Bootstrap 5**
- One-way, lightweight server → client communication

---

## 📊 Use Case

This pattern is commonly used in real-world systems such as:
- System monitoring dashboards
- Admin and DevOps portals
- Application health and metrics streaming
- Live status boards and operational views

SSE is ideal when the server needs to continuously push updates to connected clients.

---

## 🏗️ Architecture Overview

- **Backend**  
  ASP.NET Core Web API exposing an SSE endpoint: GET `/api/v1/stream/system-metrics`
- **Frontend**  
Single-page HTML + JavaScript application using: EventSource


---

## 🔄 Reconnect & Reliability

- Each SSE message includes an **event ID**
- On reconnect, the browser automatically sends `Last-Event-ID`
- The server replays missed events from an in-memory buffer
- Streaming resumes seamlessly without data loss

This makes the implementation resilient to:
- Network interruptions
- Browser refreshes
- Server restarts (within buffer limits)

---

## ▶️ How to Run

1. Restore dependencies:
 ```bash
 dotnet restore 
 ```
2. Run the application:
 ```bash
 dotnet run 
 ```
3. Open your browser and navigate to:
 ```
 http://localhost:7150/index.html
```


