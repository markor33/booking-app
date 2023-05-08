# Use an official Python runtime as a parent image
FROM python:3.11-slim as builder

# Set the working directory
WORKDIR /app

# Copy requirements.txt into the container
COPY requirements.txt ./

# Install any needed packages specified in requirements.txt
RUN pip install --trusted-host pypi.python.org --no-cache-dir --upgrade pip && \
    pip install --trusted-host pypi.python.org --no-cache-dir --user -r requirements.txt

# Start a new stage for the final image
FROM python:3.11-slim

# Set the working directory
WORKDIR /app

# Copy the installed packages from the builder stage
COPY --from=builder /root/.local /root/.local

# Make sure scripts in .local are usable:
ENV PATH=/root/.local/bin:$PATH

# Copy application code into the container
COPY . .

# Expose the port the app runs on
EXPOSE 5000

# Start the Flask app
CMD [ "python", "./app.py" ]
