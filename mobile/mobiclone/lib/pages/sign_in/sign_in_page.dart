import 'package:flutter/material.dart';

class SignInPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.white,
        iconTheme: IconThemeData(
          color: Colors.deepPurple,
        ),
        elevation: 0.0,
      ),
      backgroundColor: Colors.white,
      body: SafeArea(
        child: SingleChildScrollView(
          padding: EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              Center(
                child: Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(5.0),
                    color: Colors.deepPurple,
                  ),
                  height: 80,
                  width: 80,
                  child: Icon(
                    Icons.monetization_on,
                    color: Colors.white,
                    size: 40.0,
                  ),
                ),
              ),
              SizedBox(
                height: 24.0,
              ),
              Center(
                child: Text(
                  'Sign in to start',
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
                onPressed: () {},
                child: Text(
                  'Sign in with email',
                  style: TextStyle(
                    fontSize: 20.0,
                  ),
                ),
              ),
              SizedBox(
                height: 24.0,
              ),
              OutlineButton(
                padding: EdgeInsets.all(24.0),
                onPressed: () {
                  Navigator.of(context).pop();
                },
                child: Text(
                  'Sign up',
                  style: TextStyle(
                    fontSize: 20.0,
                  ),
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
