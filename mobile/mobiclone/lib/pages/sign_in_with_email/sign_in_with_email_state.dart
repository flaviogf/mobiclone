import 'package:equatable/equatable.dart';

abstract class SignInWithEmailState extends Equatable {}

class InitiatedSignInWithEmailState extends SignInWithEmailState {
  @override
  List<Object> get props => [];
}

class SubmittedSignInWithEmailState extends SignInWithEmailState {
  @override
  List<Object> get props => [];
}

class EmailRequiredSignInWithEmailState extends SignInWithEmailState {
  final String value;
  final String error;

  EmailRequiredSignInWithEmailState(this.value, this.error);

  @override
  List<Object> get props => [value, error];
}

class PasswordRequiredSignInWithEmailState extends SignInWithEmailState {
  final String value;
  final String error;

  PasswordRequiredSignInWithEmailState(this.value, this.error);

  @override
  List<Object> get props => [value, error];
}

class UnauthorizedSignInWithEmailState extends SignInWithEmailState {
  final String error;

  UnauthorizedSignInWithEmailState(this.error);

  @override
  List<Object> get props => [error];
}

class AuthorizedSignInWithEmailState extends SignInWithEmailState {
  @override
  List<Object> get props => [];
}
