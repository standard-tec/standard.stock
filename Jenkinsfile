
pipeline {
    
	agent any
	
    stages {
        stage('checkout') {
            steps {
		cleanWs()
                git credentialsId: 'GitHub', url: 'https://github.com/${ORGANIZATION_NAME}/${SERVICE_NAME}.git'
	   }
	}			
    }
}
