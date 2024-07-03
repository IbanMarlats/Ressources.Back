#! /bin/sh

echo "Which node version do you want to use ?"
read version

echo "UPDATE AND UPGRADE"
sudo apt update && sudo apt upgrade -y

echo "INSTALL GIT"
sudo apt install git

echo "INSTALL NODE AND NPM WITH PPA"
curl -fsSL https://deb.nodesource.com/setup_$version.x | sudo -E bash -

sudo apt update

sudo apt install nodejs

echo "###################################"
node -v
npm -v


echo "Select your node process Handler ?"
echo "[1] for Nodemon"
echo "[2] for pm2"

read choice

if [ $choice == "1" ]; then
    sudo npm i -g nodemon
else
    sudo npm i -g pm2
fi

exit 0