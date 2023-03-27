FROM node:18-alpine as node

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:latest
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /app/dist/flight-booking /usr/share/nginx/html

EXPOSE 80