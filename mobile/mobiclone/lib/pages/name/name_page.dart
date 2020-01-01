import 'package:flutter/material.dart';

class NamePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.white,
        elevation: 0,
        iconTheme: IconThemeData(
          color: Colors.deepPurple,
        ),
      ),
      backgroundColor: Colors.white,
      body: SafeArea(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: <Widget>[
            Padding(
              padding: EdgeInsets.all(16.0),
              child: Text(
                'Enter your name',
                style: TextStyle(
                  color: Colors.black54,
                  fontSize: 36.0,
                ),
              ),
            ),
            Expanded(
              flex: 1,
              child: Padding(
                padding: EdgeInsets.all(16.0),
                child: TextField(
                  textAlignVertical: TextAlignVertical.center,
                  decoration: InputDecoration(
                    border: InputBorder.none,
                    hintText: 'João',
                    hintStyle: TextStyle(
                      color: Colors.black26,
                    ),
                  ),
                  style: TextStyle(
                    color: Colors.black54,
                    fontSize: 36.0,
                  ),
                  maxLines: null,
                  expands: true,
                ),
              ),
            ),
            RaisedButton(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(0),
              ),
              padding: EdgeInsets.all(24.0),
              onPressed: () {},
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  Text(
                    'Next',
                    style: TextStyle(
                      fontSize: 20.0,
                    ),
                  ),
                  Icon(
                    Icons.arrow_forward,
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}
