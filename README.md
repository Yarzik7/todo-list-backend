## Installation

1. **Clone the repository to your local machine:**

   ```bash
   git clone https://github.com/Yarzik7/todo-list-backend.git
   cd todo-list-backend
   ```

2. **Install the dependencies:**

   ```bash
   dotnet restore
   ```

3. **Setting environment variables:**

   At the root of the project, create an .env file and add the following variables:

   DB_URI=mongodb+srv://your_name:your_password@cluster1.4jgoi.mongodb.net/?retryWrites=true&w=majority&appName=your_cluster (for MongoDB example)

   DATABASE_NAME=your database name
   
   TASKS_COLLECTION_NAME=your tasks collection

4. **Running the project:**

   ```bash
   dotnet run
   ```

Next, you can view the work of the server at http://localhost:5029