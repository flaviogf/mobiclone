import 'package:flutter/material.dart';

class SignUpPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: SafeArea(
        child: SingleChildScrollView(
          padding: EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              Center(
                child: Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(5.0),
                    color: Colors.deepPurple,
                  ),
                  child: Icon(
                    Icons.monetization_on,
                    color: Colors.white,
                    size: 40.0,
                  ),
                  height: 80,
                  width: 80,
                ),
              ),
              SizedBox(
                height: 24.0,
              ),
              Center(
                child: Text(
                  'Sign up to start',
                  style: TextStyle(
                    color: Colors.black54,
                    fontSize: 32.0,
                  ),
                ),
              ),
              SizedBox(
                height: 40.0,
              ),
              RaisedButton(
                padding: EdgeInsets.all(24.0),
                child: Text(
                  'Sign up with Facebook',
                  style: TextStyle(
                    fontSize: 20.0,
                  ),
                ),
              ),
              SizedBox(
                height: 12.0,
              ),
              RaisedButton(
                padding: EdgeInsets.all(24.0),
                child: Text(
                  'Sign up with Google',
                  style: TextStyle(
                    fontSize: 20.0,
                  ),
                ),
              ),
              SizedBox(
                height: 12.0,
              ),
              RaisedButton(
                onPressed: () {},
                padding: EdgeInsets.all(24.0),
                child: Text(
                  'Sign up with email',
                  style: TextStyle(
                    fontSize: 20.0,
                  ),
                ),
              ),
              SizedBox(
                height: 24.0,
              ),
              OutlineButton(
                onPressed: () {},
                padding: EdgeInsets.all(24.0),
                child: Text(
                  'Sign in',
                  style: TextStyle(
                    fontSize: 20.0,
                    color: Colors.deepPurpleAccent,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
