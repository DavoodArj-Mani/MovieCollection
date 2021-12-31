FROM node:14.15
RUN mkdir /usr/src/app 
WORKDIR /usr/src/app
RUN npm install -g @angular/cli
COPY . .
CMD ["npm", "start"]