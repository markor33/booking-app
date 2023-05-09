# Use a Maven builder image
FROM maven:3.8.3-openjdk-11 as builder
WORKDIR /app

# Copy the pom.xml file and download dependencies
COPY ./src/Services/Accomodation/pom.xml ./pom.xml
RUN mvn dependency:tree

# Copy the src folder and build the project
COPY ./src/Services/Accomodation/src ./src

# Build the project
RUN mvn clean install -DskipTests

# Use an OpenJDK image for running the application
FROM openjdk:11-jre-slim
WORKDIR /app
COPY --from=builder /app/target/accomodation.jar ./app.jar
ENTRYPOINT ["java", "-jar", "app.jar"]
