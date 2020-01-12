import 'package:equatable/equatable.dart';

abstract class NameState extends Equatable {}

class InitiatedNameState extends NameState {
  InitiatedNameState();

  @override
  List<Object> get props => [];
}

class SubmittedNameState extends NameState {
  final String value;

  SubmittedNameState(this.value);

  @override
  List<Object> get props => [value];
}

class ValidatedNameState extends NameState {
  final String value;

  ValidatedNameState(this.value);

  @override
  List<Object> get props => [value];
}

class RequiredNameState extends NameState {
  final String value;
  final String error;

  RequiredNameState(this.value, this.error);

  @override
  List<Object> get props => [value, error];
}
