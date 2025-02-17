# Cosmos DB NoSQL API - Global Secondary Index Demo

This project is an Azure Functions application that exposes REST APIs for managing accounts using Azure Cosmos DB as the backend.

The application provides functionalities for creating an account, retrieving an account by ID, and retrieving an account by the user's SSN.

It also includes a function to process changes from the Cosmos DB change feed to maintain a mapping of SSN to account ID - Global Secondary Index.

## Requirements

- .NET 9 SDK
- Azure Functions Core Tools
- Azure Cosmos DB account
	- accounts collection: Partition Key `/id` (id represents account id)
	- accountIndex collection: Partition Key `/id` (id represents ssn - or any unique attribute used as alternate filter)

## API Endpoints

Sample API calls are provided in `app.http` file.

### Create Account

- **Endpoint:** `POST /api/createAccount`
- **Description:** Creates a new account in the Cosmos DB.
- **Request Body:** JSON object containing account details (e.g., Name, SSN).

### Get Account by ID

- **Endpoint:** `GET /api/getAccountById/{accountId}`
- **Description:** Retrieves an account by its unique ID.
- **Path Parameter:** `accountId` - The ID of the account to retrieve.

### Get Account by SSN

- **Endpoint:** `GET /api/getAccountBySsn/{ssn}`
- **Description:** Retrieves an account by the user's SSN.
- **Path Parameter:** `ssn` - The SSN of the account to retrieve.

### Configuration

Update the `local.settings.json` file with your Cosmos DB connection string and any other necessary environment variables.

## Running the Application

1. Clone the repository.
2. Navigate to the project directory.
3. Run the application using the Azure Functions Core Tools.

## Note

> - This sample assumes a single account per SSN.
>
> - If multiple accounts are allowed per SSN, the accountIndex collection should either have another partition key to allow multiple entries per SSN or store an array of account IDs.
>
> - Global Secondary Index should be used with exact match queries.

## License

This project is licensed under the MIT License.