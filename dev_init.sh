

#!/usr/bin/env bash
set -e

echo "[dev_init.sh] Restoring .NET dependencies..."
dotnet restore

echo "[dev_init.sh] Building solution..."
dotnet build

echo "[dev_init.sh] Verifying console app runs..."
dotnet run --project ClickAdventure.Runner -- --help

echo "[dev_init.sh] Environment setup complete."