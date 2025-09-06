#!/usr/bin/env bash
set -e

echo "[dev_init.sh] Restoring .NET dependencies..."
dotnet restore

echo "[dev_init.sh] Building solution..."
dotnet build

echo "[dev_init.sh] Verifying console app runs..."
dotnet run --project ClickAdventure.Runner -- --help

echo "[dev_init.sh] Installing Playwright browsers (Chromium)..."

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
  echo "[dev_init.sh] Node.js not found. Please install Node.js to continue."
  exit 1
fi


# Check if Playwright CLI is installed
if ! npx playwright --version &> /dev/null; then
  echo "[dev_init.sh] Playwright CLI not found. Please run 'npm install' to install dependencies."
  exit 1
fi

# Install Chromium browser for Playwright
npx playwright install chromium

echo "[dev_init.sh] Environment setup complete."