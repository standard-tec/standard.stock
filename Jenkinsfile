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
            steps{
                cleanWs()
                git credentialsId: 'GitHub', url: 'https://github.com/${ORGANIZATION_NAME}/${SERVICE_NAME}.git'
            }
        }

        stage('Restore Packages') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Clean') {
            steps {
                bat 'dotnet clean'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Build and Push Image'){
            steps {
                sh 'docker image build -t ${REPOSITORY_TAG} .'
            }
        }

        stage('Deploy to Cluster') {
            steps {
                sh 'envsubst < ${WORKSPACE}/Deploy.yaml | kubectl apply -f -'
            }
        }
    }
}