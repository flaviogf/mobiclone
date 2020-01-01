import 'package:flutter/material.dart';
import 'package:mobiclone/pages/name/name_page.dart';
import 'package:mobiclone/pages/sign_in/sign_in_page.dart';

class SignUpPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.white,
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
                onPressed: () {
                  Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (_) => NamePage(),
                    ),
                  );
                },
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
                onPressed: () {
                  Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (_) => SignInPage(),
                    ),
                  );
                },
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
