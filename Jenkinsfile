﻿
pipeline {
    agent any

    environment {
        //ORGANIZATION_NAME = 'igortavares82'
        //DOCKERHUB_USERNAME='igortavares'
        SERVICE_NAME = 'standard.stock'
        REPOSITORY_TAG='${DOCKERHUB_USERNAME}/${ORGANIZATION_NAME}-${SERVICE_NAME}:${BUILD_ID}'
    }

    stages {
        stage('Checkout') {
            steps {
                git url: 'https://github.com/standard-tec/standard.stock.git'
            }
        }

        
    }
}