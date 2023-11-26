FROM node:latest

WORKDIR /app

COPY package.json package-lock.json /app/

RUN npm install

COPY . /app/

RUN npm run build

EXPOSE 3000

ENV NODE_ENV=development

STOPSIGNAL SIGINT

CMD ["npm", "run", "dev"]